using System.Collections.Generic;
using System.Linq;

public interface IGoapPlanner
{
    public ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null);
}
    public class GoapPlanner : IGoapPlanner
{
    public ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null)
    {
        List<AgentGoal> agentGoals = goals
            .Where(g => g.DesiredEffects.Any(b => !b.Evaluate()))
            .OrderByDescending(g => g == mostRecentGoal ? g.Priority - 0.01 : g.Priority)
            .ToList();

        foreach(AgentGoal goal in agentGoals)
        {
            Goap.Node goalNode = new Goap.Node(null, null, goal.DesiredEffects, 0);
        }
    }

    private bool FindPath(Goap.Node parent, HashSet<AgentAction> actions)
    {
        //コスト順にアクションを並べ替え
        IOrderedEnumerable<AgentAction> orderedActions = actions.OrderBy(a => a.Cost);

        foreach(AgentAction action in orderedActions)
        {
            HashSet<AgentBelief> requiredEffects = parent.RequiredEffects;

            //すでに達成している信念をリストから排除
            requiredEffects.RemoveWhere(b => b.Evaluate());

            //信念が積まれていない場合は終了
            if (requiredEffects.Count == 0)
            {
                return true;
            }

            //特定の信念を達成できるアクションを割り出す。
            if (action.Effects.Any(requiredEffects.Contains))
            {
                HashSet<AgentBelief> newRequiredEffects = new HashSet<AgentBelief>(requiredEffects);
                //達成するであろう信念を排除
                newRequiredEffects.ExceptWith(action.Effects);
                //アクションを実行するために必要な信念を追加
                newRequiredEffects.UnionWith(action.Preconditions);

                //目標達成につながらないアクションはスキップ（消すのではなくboolを返す関数があってもいいのでは）
                HashSet<AgentAction> newAvailableActions = new HashSet<AgentAction>(actions);
                newAvailableActions.Remove(action);

                //新たな状態になるために必要なノードの生成
                Goap.Node newNode = new Goap.Node(parent, action, newRequiredEffects, parent.Cost + action.Cost);

                //再帰的に必要なアクションを探査する
                if (FindPath(newNode, newAvailableActions))
                {
                    parent.Leaves.Add(newNode);
                    newRequiredEffects.ExceptWith(newNode.Action.Preconditions);
                }

                //新たな状態を達成するために必要な信念が残っている場合は、次のアクションを探す
                if (newRequiredEffects.Count == 0)
                {
                    return true;
                }
            }
        }

        //目標を達成するために必要な経路がある場合はtrueを返す
        return parent.Leaves.Count > 0;
    }
}

namespace Goap
{
    public class Node
    {
        public Node Parent { get; }
        public AgentAction Action { get; }
        public HashSet<AgentBelief> RequiredEffects { get; }
        public float Cost { get; }  
        public List<Node> Leaves { get; } 

        public Node(Node parent, AgentAction action, HashSet<AgentBelief> effects, float cost)
        {
            Action = action;
            Parent = parent;
            RequiredEffects = new HashSet<AgentBelief>(effects);
            Leaves = new List<Node>();
            Cost = cost;
        }
    }
}

public class ActionPlan
{
    public AgentGoal Goal { get; }
    public Stack<AgentAction> Actions { get; }
    public Node TotalNode { get; }

    public ActionPlan(AgentGoal goal, Stack<AgentAction> actions, Node totalNode)
    {
        Goal = goal;
        Actions = actions;
        TotalNode = totalNode;
    }
}


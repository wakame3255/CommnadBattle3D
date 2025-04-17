using System.Collections.Generic;
using System.Linq;

/// <summary>
/// GOAPシステムのプランナーインターフェース
/// アクションプランの生成を定義
/// </summary>
public interface IGoapPlanner
{
    /// <summary>
    /// エージェントの目標達成のためのアクションプランを生成
    /// </summary>
    /// <param name="agent">プランを生成するエージェント</param>
    /// <param name="goals">考慮する目標のセット</param>
    /// <param name="mostRecentGoal">最も直近に実行していた目標（オプション）</param>
    /// <returns>実行可能なアクションプラン、または実行可能なプランがない場合はnull</returns>
    public ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null);
}
/// <summary>
/// GOAPプランナーの実装クラス
/// A*アルゴリズムを用いて最適なアクションプランを生成
/// </summary>
public class GoapPlanner : IGoapPlanner
{
    public ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null)
    {
        // 未達成の目標を優先度でソート
        // 直近の目標は若干優先度を下げて継続性を確保
        List<AgentGoal> agentGoals = goals
            .Where(g => g.DesiredEffects.Any(b => !b.Evaluate()))
            .OrderByDescending(g => g == mostRecentGoal ? g.Priority - 0.01f : g.Priority)
            .ToList();

        foreach (AgentGoal goal in agentGoals)
        {
            // 目標達成に必要な状態をノードとして作成
            Goap.Node goalNode = new Goap.Node(null, null, goal.DesiredEffects, 0);

            if (FindPath(goalNode, agent.Actions))
            {
                // 有効なパスが見つからない場合は次の目標へ
                if (goalNode.IsLeafDead)
                {
                    continue;
                }

                // 最小コストのアクションシーケンスを構築
                Stack<AgentAction> actionStack = new Stack<AgentAction>();
                while (goalNode.Leaves.Count > 0)
                {
                    Goap.Node lowestCostLeaf = goalNode.Leaves.OrderBy(leaf => leaf.Cost).First();
                    goalNode = lowestCostLeaf;
                    actionStack.Push(lowestCostLeaf.Action);
                }

                return new ActionPlan(goal, actionStack, goalNode.Cost);
            }
        }

        DebugUtility.Log("No valid action plan found.");
        return null;
    }

    /// <summary>
    /// A*アルゴリズムを用いて目標達成までのアクションパスを探索
    /// </summary>
    /// <param name="parent">探索の開始ノード</param>
    /// <param name="actions">利用可能なアクションのセット</param>
    /// <returns>有効なパスが見つかった場合はtrue</returns>
    private bool FindPath(Goap.Node parent, HashSet<AgentAction> actions)
    {
        // コスト順にアクションをソート
        IOrderedEnumerable<AgentAction> orderedActions = actions.OrderBy(a => a.Cost);

        foreach (AgentAction action in orderedActions)
        {
            HashSet<AgentBelief> requiredEffects = parent.RequiredEffects;

            // 既に達成している効果を除外
            requiredEffects.RemoveWhere(b => b.Evaluate());

            // すべての効果が達成済みの場合は探索成功
            if (requiredEffects.Count == 0)
            {
                return true;
            }

            // 目標の効果を達成できるアクションを検証
            if (action.Effects.Any(requiredEffects.Contains))
            {
                // 新しい要求効果セットを作成
                HashSet<AgentBelief> newRequiredEffects = new HashSet<AgentBelief>(requiredEffects);
                // アクションの効果により達成される効果を除外
                newRequiredEffects.ExceptWith(action.Effects);
                // アクションの前提条件を新たな要求効果として追加
                newRequiredEffects.UnionWith(action.Preconditions);

                // 使用可能なアクションから現在のアクションを除外
                HashSet<AgentAction> newAvailableActions = new HashSet<AgentAction>(actions);
                newAvailableActions.Remove(action);

                // 新しい状態ノードを作成
                Goap.Node newNode = new Goap.Node(parent, action, newRequiredEffects, parent.Cost + action.Cost);

                // 再帰的にパスを探索
                if (FindPath(newNode, newAvailableActions))
                {
                    parent.Leaves.Add(newNode);
                    newRequiredEffects.ExceptWith(newNode.Action.Preconditions);
                }

                // すべての要求効果が満たされた場合は探索成功
                if (newRequiredEffects.Count == 0)
                {
                    return true;
                }
            }
        }

        // 有効な子ノードが存在する場合は探索成功
        return parent.Leaves.Count > 0;
    }
}

namespace Goap
{
    /// <summary>
    /// GOAPの探索木におけるノードクラス
    /// アクションのチェーンと状態遷移を表現
    /// </summary>
    public class Node
    {
        /// <summary>
        /// 親ノード
        /// </summary>
        public Node Parent { get; }

        /// <summary>
        /// このノードに関連付けられたアクション
        /// </summary>
        public AgentAction Action { get; }

        /// <summary>
        /// 目標達成に必要な効果のセット
        /// </summary>
        public HashSet<AgentBelief> RequiredEffects { get; }

        /// <summary>
        /// このノードまでの累積コスト
        /// </summary>
        public float Cost { get; }

        /// <summary>
        /// 子ノードのリスト
        /// </summary>
        public List<Node> Leaves { get; }

        /// <summary>
        /// このノードが行き止まりかどうか
        /// </summary>
        public bool IsLeafDead => Leaves.Count == 0 && Action == null;

        /// <summary>
        /// ノードの初期化
        /// </summary>
        /// <param name="parent">親ノード</param>
        /// <param name="action">関連するアクション</param>
        /// <param name="effects">必要な効果のセット</param>
        /// <param name="cost">累積コスト</param>
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

/// <summary>
/// 実行可能なアクションのシーケンスを表現するクラス
/// </summary>
public class ActionPlan
{
    /// <summary>
    /// このプランが達成しようとする目標
    /// </summary>
    public AgentGoal Goal { get; }

    /// <summary>
    /// 実行すべきアクションのスタック
    /// </summary>
    public Stack<AgentAction> Actions { get; }

    /// <summary>
    /// プラン全体の実行コスト
    /// </summary>
    public float TotalCost { get; }

    /// <summary>
    /// アクションプランの初期化
    /// </summary>
    /// <param name="goal">達成する目標</param>
    /// <param name="actions">実行するアクションのスタック</param>
    /// <param name="totalCost">プラン全体のコスト</param>
    public ActionPlan(AgentGoal goal, Stack<AgentAction> actions, float totalCost)
    {
        Goal = goal;
        Actions = actions;
        TotalCost = totalCost;
    }
}


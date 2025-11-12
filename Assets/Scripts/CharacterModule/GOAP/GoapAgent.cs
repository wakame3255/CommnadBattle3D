using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GOAP（Goal-Oriented Action Planning）エージェント
/// AIキャラクターの目標指向型行動計画を管理
/// </summary>
public class GoapAgent
{
    private AllCharacterStatus _allCharacterStatus;

    private CountdownTimer _statusTimer;

    private AgentGoal _lastGoal;

    private AgentGoal _currentGoal;

    private ActionPlan _actionPlan;

    private AgentAction _currentAction;

    private Dictionary<string, AgentBelief> _beliefs;

    private HashSet<AgentGoal> _goals;

    /// <summary>
    /// エージェントが実行可能なアクションのセット
    /// </summary>
    public HashSet<AgentAction> Actions { get; private set; } = new HashSet<AgentAction>();

    /// <summary>
    /// エージェントのTransform
    /// </summary>
    public Transform MyTransform { get; }

    IGoapPlanner _goapPlanner;

    /// <summary>
    /// コンストラクタ
    /// GOAP エージェントの初期化
    /// </summary>
    /// <param name="allCharacterStatus">全キャラクターのステータス情報</param>
    /// <param name="goapFactory">GOAPプランナーファクトリー</param>
    /// <param name="myTransform">このエージェントのTransform</param>
    public GoapAgent(AllCharacterStatus allCharacterStatus, GoapFactory goapFactory, Transform myTransform)
    {
        _allCharacterStatus = allCharacterStatus;
        _goapPlanner = goapFactory.CreateGoapPlanner();
        MyTransform = myTransform;

        SetupBeliefs();
        SetupAction();
        SetupGoals();
    }

    /// <summary>
    /// エージェントの信念（状態認識）を設定
    /// </summary>
    private void SetupBeliefs()
    {
        _beliefs = new Dictionary<string, AgentBelief>();
        BeliefFactory beliefFactory = new BeliefFactory(this, _beliefs);

        beliefFactory.AddBelief("Nothing", () => false);

        beliefFactory.AddBelief
            ("AgentHealthLow", () => _allCharacterStatus.MyCharacterStatus.RPHealth.CurrentValue <= 5);
        beliefFactory.AddBelief
            ("AgentHealthHigh", () => _allCharacterStatus.MyCharacterStatus.RPHealth.CurrentValue > 5);
        beliefFactory.AddBelief
            ("TargetInAttackRange", () => false);
        beliefFactory.AddBelief
            ("AttackingTarget", () => false);
    }

    /// <summary>
    /// エージェントが実行可能なアクションを設定
    /// </summary>
    private void SetupAction()
    {
        Actions = new HashSet<AgentAction>();

        Actions.Add(new AgentAction.Builder("AttackPlayer")
            .WithActionStrategy(new AttackStrategy())
            .AddPrecondition(_beliefs["TargetInAttackRange"])
            .AddEffect(_beliefs["AttackingTarget"])
            .Build());
    }

    /// <summary>
    /// エージェントの目標を設定
    /// </summary>
    private void SetupGoals()
    {
        _goals = new HashSet<AgentGoal>();
        _goals.Add(new AgentGoal.Builder("SeekAndDestroy")
            .WithPriority(1)
            .WithEffect(_beliefs["AttackingTarget"])
            .Build());
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

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

    public HashSet<AgentAction> Actions { get; private set; } = new HashSet<AgentAction>();

    public Transform MyTransform { get; }

    IGoapPlanner _goapPlanner;

    public GoapAgent(AllCharacterStatus allCharacterStatus, GoapFactory goapFactory, Transform myTransform)
    {
        _allCharacterStatus = allCharacterStatus;
        _goapPlanner = goapFactory.CreateGoapPlanner();
        MyTransform = myTransform;

        SetupBeliefs();
        SetupAction();
        SetupGoals();
    }

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

    private void SetupAction()
    {
        Actions = new HashSet<AgentAction>();

        Actions.Add(new AgentAction.Builder("AttackPlayer")
            .WithActionStrategy(new AttackStrategy())
            .AddPrecondition(_beliefs["TargetInAttackRange"])
            .AddEffect(_beliefs["AttackingTarget"])
            .Build());
    }

    private void SetupGoals()
    {
        _goals = new HashSet<AgentGoal>();
        _goals.Add(new AgentGoal.Builder("SeekAndDestroy")
            .WithPriority(1)
            .WithEffect(_beliefs["AttackingTarget"])
            .Build());
    }
}

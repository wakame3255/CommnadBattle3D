using System;
using UnityEngine;
using R3;
using System.Collections.Generic;

public class BeliefFactory
{
    readonly GoapAgent _agent;
    readonly Dictionary<string, AgentBelief> _beliefs;

    public BeliefFactory(GoapAgent agent, Dictionary<string, AgentBelief> belief)
    {
        this._agent = agent;
        this._beliefs = belief;
    }

    public void AddBelief(string key, ReactiveProperty<bool> condition)
    {
        _beliefs.Add(key, new AgentBelief.Builder(key)
            .WithCondition(condition)
            .Build());
    }

    public void AddLocationBelief(string key, float distance, Vector3 locationCondition)
    {
        //_beliefs.Add(key, new AgentBelief.Builder(key)
        //    .WithCondition(InRangeOf(locationCondition, distance))
    }

    bool InRangeOf(Vector3 pos, float range) => Vector3.Distance(_agent.transform.position, pos) < range;
}

public class AgentBelief
{
    public string Name { get;}

    /// <summary>
    /// エージェントの状態
    /// </summary>
    private ReactiveProperty<bool> _rPCondition = new ReactiveProperty<bool>(false);
    public ReadOnlyReactiveProperty<bool> RPCondition => _rPCondition;

    /// <summary>
    /// エージェントの位置
    /// </summary>
    private ReactiveProperty<Vector3> _observedLocation = new ReactiveProperty<Vector3>(Vector3.zero);
    public ReadOnlyReactiveProperty<Vector3> ObservedLocation => _observedLocation;

    AgentBelief(string name)
    {
        Name = name;
    }

    public bool Evaluate() => _rPCondition.Value;

    public class Builder
    {
        readonly AgentBelief _belief;

        public Builder(string name)
        {
            _belief = new AgentBelief(name);
        }

        public Builder WithCondition(ReactiveProperty<bool> condition)
        {
            _belief._rPCondition = condition;
            return this;
        }

        public Builder WithLocation(ReactiveProperty<Vector3> location)
        {
            _belief._observedLocation = location;
            return this;
        }

        public AgentBelief Build()
        {
            return _belief;
        }
    }
}

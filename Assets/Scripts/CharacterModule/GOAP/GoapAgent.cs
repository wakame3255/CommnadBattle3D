using System;
using UnityEngine;
using System.Collections.Generic;
public class GoapAgent : MonoBehaviour 
{

    //現在の状態
    private Dictionary<WorldStateKey, object> _worldState = new Dictionary<WorldStateKey, object>();

    //目標状態
    private Dictionary<WorldStateKey, object> _goal = new Dictionary<WorldStateKey, object>();

    //アクションのリスト
    private List<GoapActionBase> _actions = new List<GoapActionBase>();

    //現在のアクション
    private Queue<GoapActionBase> _currentPlan;

    private void Awake()
    {
        //アクションの生成
        _actions.Add(gameObject.AddComponent<AttackForEnemyAction>());
        _actions.Add(gameObject.AddComponent<ApproachForEnemyAction>());

        foreach(GoapActionBase goapAction in _actions)
        {
            goapAction.Setup();
        }

        //初期状態
        _worldState.Add(WorldStateKey.EnemyIsInRange, false);

        //ゴールの設定
        _goal = new Dictionary<WorldStateKey, object>();
        _goal.Add(WorldStateKey.EnemyIsInRange, true);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (_currentPlan == null || _currentPlan.Count == 0)
        {

        }
    }


}

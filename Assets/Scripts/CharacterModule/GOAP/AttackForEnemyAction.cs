using System;
using UnityEngine;
using System.Collections.Generic;
public class AttackForEnemyAction : GoapActionBase
{
    private bool _attacked = false;

    public override void Setup()
    {
        ActionName = "敵を攻撃する";
        Cost = 1f;

        //前提条件
        Preconditions.Add(WorldStateKey.EnemyIsInRange, true);
    }

    public override bool Perform(GameObject agent, Dictionary<WorldStateKey, object> worldState)
    {
        DebugUtility.Log("敵を攻撃するアクションを実行中");
        _attacked = true;

        return true;
    }

    public override bool IsActionDone()
    {
        return _attacked;
    }

    public override void Reset()
    {
        _attacked = false;
    }

    public override bool CheckProcedures(GameObject agent)
    {
        // 本当に攻撃範囲にいるのかの確認を行う
        return true;
    }
}

using System;
using UnityEngine;
using System.Collections.Generic;
public class ApproachForEnemyAction : GoapActionBase
{
    private bool _isInRange = false;

    public override void Setup()
    {
        ActionName = "敵に近づく";
        Cost = 3f;

        //効果
        Effects.Add(WorldStateKey.EnemyIsInRange, true);
    }

    public override bool Perform(GameObject agent, Dictionary<WorldStateKey, object> worldState)
    {
        DebugUtility.Log("敵に近づくアクションを実行中");

        // 近づく処理をここに実装
        _isInRange = true;

        worldState[WorldStateKey.EnemyIsInRange] = true;

        return true;
    }

    public override bool IsActionDone()
    {
        return _isInRange;
    }

    public override void Reset()
    {
        _isInRange = false;
    }
}

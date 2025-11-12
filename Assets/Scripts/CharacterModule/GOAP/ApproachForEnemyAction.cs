using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 敵への接近アクション
/// GOAPシステムで使用する敵に近づくアクション
/// </summary>
public class ApproachForEnemyAction : GoapActionBase
{
    private bool _isInRange = false;

    /// <summary>
    /// アクションの初期設定
    /// コストと効果を設定
    /// </summary>
    public override void Setup()
    {
        ActionName = "敵に近づく";
        Cost = 3f;

        //効果
        Effects.Add(WorldStateKey.EnemyIsInRange, true);
    }

    /// <summary>
    /// アクションを実行
    /// </summary>
    /// <param name="agent">実行するエージェント</param>
    /// <param name="worldState">ワールド状態</param>
    /// <returns>実行成功の可否</returns>
    public override bool Perform(GameObject agent, Dictionary<WorldStateKey, object> worldState)
    {
        DebugUtility.Log("敵に近づくアクションを実行中");

        // 近づく処理をここに実装
        _isInRange = true;

        worldState[WorldStateKey.EnemyIsInRange] = true;

        return true;
    }

    /// <summary>
    /// アクションが完了したかチェック
    /// </summary>
    /// <returns>完了していればtrue</returns>
    public override bool IsActionDone()
    {
        return _isInRange;
    }

    /// <summary>
    /// アクションをリセット
    /// </summary>
    public override void Reset()
    {
        _isInRange = false;
    }
}

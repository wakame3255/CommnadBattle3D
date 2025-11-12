using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 敵への攻撃アクション
/// GOAPシステムで使用する敵を攻撃するアクション
/// </summary>
public class AttackForEnemyAction : GoapActionBase
{
    private bool _attacked = false;

    /// <summary>
    /// アクションの初期設定
    /// コストと前提条件を設定
    /// </summary>
    public override void Setup()
    {
        ActionName = "敵を攻撃する";
        Cost = 1f;

        //前提条件
        Preconditions.Add(WorldStateKey.EnemyIsInRange, true);
    }

    /// <summary>
    /// アクションを実行
    /// </summary>
    /// <param name="agent">実行するエージェント</param>
    /// <param name="worldState">ワールド状態</param>
    /// <returns>実行成功の可否</returns>
    public override bool Perform(GameObject agent, Dictionary<WorldStateKey, object> worldState)
    {
        DebugUtility.Log("敵を攻撃するアクションを実行中");
        _attacked = true;

        return true;
    }

    /// <summary>
    /// アクションが完了したかチェック
    /// </summary>
    /// <returns>完了していればtrue</returns>
    public override bool IsActionDone()
    {
        return _attacked;
    }

    /// <summary>
    /// アクションをリセット
    /// </summary>
    public override void Reset()
    {
        _attacked = false;
    }

    /// <summary>
    /// 前提条件を確認
    /// </summary>
    /// <param name="agent">実行するエージェント</param>
    /// <returns>前提条件を満たしていればtrue</returns>
    public override bool CheckProcedures(GameObject agent)
    {
        // 本当に攻撃範囲にいるのかの確認を行う
        return true;
    }
}

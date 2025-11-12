using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 近接攻撃のモデルクラス
/// 近接攻撃の範囲、ダメージ、対象選定のロジックを管理
/// </summary>
public class MeleeAttackModel : ActionModelBase
{

    /// <summary>
    /// コンストラクタ
    /// 近接攻撃のパラメータを初期化
    /// </summary>
    /// <param name="attackService">攻撃処理サービス</param>
    public MeleeAttackModel(IAttackHandler attackService)
    {
        _attackRange = 5.0f;
        _scopeOfEffect = 1.0f;
        _actionCost = 1;

        _attackService = attackService; 
    }
    
    /// <summary>
    /// 近接攻撃を実行
    /// </summary>
    /// <param name="targets">攻撃対象のコライダーリスト</param>
    public override void DoAction(List<Collider> targets)
    {
        DebugUtility.Log("MeleeAttackModel DoAction");
        _attackService.ExecuteAttack(targets, 10);
    }

    /// <summary>
    /// 攻撃可能な対象をチェック
    /// </summary>
    public override void CheckActionTarget()
    {
        DebugUtility.Log("MeleeAttackModel CheckActionTarget");
        List<Collider> targets = _attackService.ReturnScopeTarget(_actionNotifier.CharacterPos, _attackRange, _owner.Faction);
        _actionNotifier.SetScopeTarget(targets);
    }
}

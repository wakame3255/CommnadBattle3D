using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃処理を扱うインターフェース
/// 攻撃の実行と範囲内の対象取得を提供
/// </summary>
public interface IAttackHandler
{
    /// <summary>
    /// 攻撃を実行
    /// </summary>
    /// <param name="attackTarget">攻撃対象のコライダーリスト</param>
    /// <param name="damage">与えるダメージ量</param>
    public void ExecuteAttack(List<Collider> attackTarget, int damage);

    /// <summary>
    /// 攻撃範囲内の対象を取得
    /// </summary>
    /// <param name="attackPosition">攻撃位置</param>
    /// <param name="attackRange">攻撃範囲</param>
    /// <param name="owner">攻撃者の陣営</param>
    /// <returns>範囲内の対象コライダーリスト</returns>
    public List<Collider> ReturnScopeTarget(Vector3 attackPosition, float attackRange, Faction owner);
}

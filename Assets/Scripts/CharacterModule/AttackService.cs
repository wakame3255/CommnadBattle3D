 
using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackService
{
    private Dictionary<Collider, IDamageNotice> _damageNoticeMap = default;

    public AttackService()
    {
        _damageNoticeMap = new Dictionary<Collider, IDamageNotice>();
    }

    /// <summary>
    /// 攻撃リクエスト
    /// </summary>
    private void RequestForAttack(Collider collider, int damage)
    {
        // ダメージ通知
        if (_damageNoticeMap.TryGetValue(collider, out var damageNotice))
        {
            damageNotice.NotifyDamage(damage);
        }
        else
        {
            Debug.LogError("Collider is not be found in damage notice map.");
        }
    }

    /// <summary>
    ///  攻撃を行うための判定処理
    /// </summary>
    /// <param name="targetPosition">攻撃を行う地点</param>
    /// <param name="attackRange">攻撃範囲</param>
    /// <param name="damage">攻撃力</param>
    public void ExecuteAttack(Vector3 targetPosition, float attackRange, int damage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(targetPosition, attackRange);

        foreach (Collider hitCollder in hitColliders)
        {
            RequestForAttack(hitCollder, damage);
        }
    }

    /// <summary>
    /// キャラクターの追加
    /// </summary>
    /// <param name="damageNotice"></param>
    public void AddDamageNotice(IDamageNotice damageNotice, Collider collider)
    {
        _damageNoticeMap.Add(collider, damageNotice);
    }


}
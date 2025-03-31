 
using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackCalculation
{
    private Dictionary<Collider, IDamageNotice> _damageNoticeMap = default;

    public AttackCalculation()
    {
        _damageNoticeMap = new Dictionary<Collider, IDamageNotice>();
    }

    /// <summary>
    /// 攻撃リクエスト
    /// </summary>
    public void RequestForAttack(Collider collider, int damage)
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
    /// キャラクターの追加
    /// </summary>
    /// <param name="damageNotice"></param>
    public void AddDamageNotice(IDamageNotice damageNotice, Collider collider)
    {
        _damageNoticeMap.Add(collider, damageNotice);
    }
}
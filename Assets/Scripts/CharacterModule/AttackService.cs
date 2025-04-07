using System.Collections.Generic;
using UnityEngine;

public class AttackService : IAttackHandler
{
    private Dictionary<Collider, (IDamageNotice, IFactionMember)> _damageNoticeMap = default;

    public AttackService()
    {
        _damageNoticeMap = new Dictionary<Collider, (IDamageNotice, IFactionMember)>();
    }

    /// <summary>
    /// 攻撃リクエスト
    /// </summary>
    private void RequestForAttack(Collider collider, int damage, Faction owner)
    {
        //辞書にいるキャラかの判断
        if (!_damageNoticeMap.TryGetValue(collider, out (IDamageNotice Damage, IFactionMember Faction) hitCharacter))
        {
            return;        
        }

        //自分の陣営の場合は攻撃しない
        if (hitCharacter.Faction.Faction == owner)
        {
            DebugUtility.Log("My Friend");
            return;
        }

        //ダメージを与える
        hitCharacter.Damage.NotifyDamage(damage);
        DebugUtility.Log(collider.name + damage);
    }

    /// <summary>
    ///  攻撃を行うための判定処理
    /// </summary>
    /// <param name="attackPosition">攻撃を行う地点</param>
    /// <param name="attackRange">攻撃範囲</param>
    /// <param name="damage">攻撃力</param>
    public Collider[] ExecuteAttack(Vector3 attackPosition, float attackRange, int damage, Faction owner)
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPosition, attackRange);

        foreach (Collider hitCollider in hitColliders)
        {
            RequestForAttack(hitCollider, damage, owner);
        }

        return hitColliders;
    }

    /// <summary>
    /// キャラクターを辞書に登録
    /// </summary>
    /// <param name="damageNotice"></param>
    public void AddDamageNotice(CharacterStatusModel characterStatus, Collider collider)
    {
        _damageNoticeMap.Add(collider, (characterStatus, characterStatus));
    }


}

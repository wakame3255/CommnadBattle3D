using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

public class AttackService : IAttackHandler
{
    private Dictionary<Collider, (IDamageNotice, IFactionMember)> _damageNoticeMap = default;

    public AttackService()
    {
        _damageNoticeMap = new Dictionary<Collider, (IDamageNotice, IFactionMember)>();
    }

    /// <summary>
    ///  攻撃を行うための判定処理
    /// </summary>
    /// <param name="damage">攻撃力</param>
    public void ExecuteAttack(List<Collider> attackTarget, int damage)
    {
        foreach (Collider target in attackTarget)
        {
            if (_damageNoticeMap.TryGetValue(target, out (IDamageNotice Damage, IFactionMember Faction) hitCharacter))
            {
                //ダメージを与える
                hitCharacter.Damage.NotifyDamage(damage);
                DebugUtility.Log(target.ToString() + damage);
            }             
        }
    }

    /// <summary>
    /// 攻撃範囲を取得する
    /// </summary>
    /// <param name="attackPosition"></param>
    /// <param name="attackRange"></param>
    /// <param name="owner"></param>
    /// <returns>範囲内の対象</returns>
    public List<Collider> ReturnScopeTarget(Vector3 attackPosition, float attackRange, Faction owner)
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPosition, attackRange);

        return ReturnOtherThanOneselfStatus(hitColliders.ToList(), owner);
    }

    /// <summary>
    /// 辞書に登録
    /// </summary>
    /// <param name="characterStatus"></param>
    /// <param name="collider"></param>
    public void AddDamageNotice(CharacterStatusModel characterStatus, Collider collider)
    {
        _damageNoticeMap.Add(collider, (characterStatus, characterStatus));
    }

    /// <summary>
    /// 攻撃リクエスト
    /// </summary>
    private void RequestForAttack(List<Collider> targets, int damage)
    {
      //使う可能性
    }

    /// <summary>
    /// 自分以外のキャラクターを取得する
    /// </summary>
    /// <param name="checkTargets"></param>
    /// <param name="owner"></param>
    /// <returns>自身を抜いたターゲット</returns>
    private List<Collider> ReturnOtherThanOneselfStatus(List<Collider> checkTargets, Faction owner)
    {
        List<Collider> targets = new List<Collider>();

        foreach (Collider target in checkTargets)
        {
            //辞書にいるキャラかの判断
            if (!_damageNoticeMap.TryGetValue(target, out (IDamageNotice Damage, IFactionMember Faction) hitCharacter))
            {
                continue;
            }

            //自分の陣営以外の格納
            if (hitCharacter.Faction.Faction != owner)
            {
                targets.Add(target);
            }
        }

        return targets;
    }
}

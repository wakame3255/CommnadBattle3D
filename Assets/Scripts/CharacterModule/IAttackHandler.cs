using System;
using UnityEngine;

public interface IAttackHandler
{
    public Collider[] ExecuteAttack(Vector3 attackPosition, float attackRange, int damage, Faction owner);
}

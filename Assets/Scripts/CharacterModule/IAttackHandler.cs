using System;
using UnityEngine;

public interface IAttackHandler
{
    public void ExecuteAttack(Vector3 attackPosition, float attackRange, int damage, Faction owner);
}

using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackHandler
{
    public void ExecuteAttack(List<Collider> attackTarget, int damage);

    public List<Collider> ReturnScopeTarget(Vector3 attackPosition, float attackRange, Faction owner);
}

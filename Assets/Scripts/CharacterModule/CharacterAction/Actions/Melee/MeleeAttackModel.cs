using System;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackModel : ActionModelBase
{


    public MeleeAttackModel(IAttackHandler attackService)
    {
        _attackRange = 5.0f;
        _scopeOfEffect = 1.0f;
        _actionCost = 1;

        _attackService = attackService; 
    }
    public override void DoAction(List<Collider> targets)
    {
        _attackService.ExecuteAttack(targets, 10);
    }

    public override List<Collider> CheckActionTarget()
    {
        List<Collider> targets = _attackService.ReturnScopeTarget(_actionNotifier.CharacterPos, _attackRange, _owner.Faction);
        _actionNotifier.SetScopeTarget(targets);

        return targets;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackModel : ActionModelBase
{


    public MeleeAttackModel(IAttackHandler attackService)
    {
        _attackRange = 5.0f;
        _scopeOfEffect = 1.0f;

        _attackService = attackService; 
    }
    public override void DoAction(List<Collider> targets)
    {
        DebugUtility.Log("MeleeAttackModel DoAction");
        _attackService.ExecuteAttack(targets, 10);
    }

    public override void CheckActionTarget()
    {
        DebugUtility.Log("MeleeAttackModel CheckActionTarget");
        List<Collider> targets = _attackService.ReturnScopeTarget(_actionNotifier.CharacterPos, _attackRange, _owner.Faction);
        _actionNotifier.SetScopeTarget(targets);
    }
}

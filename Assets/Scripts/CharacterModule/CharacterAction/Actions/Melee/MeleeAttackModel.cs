using System;

public class MeleeAttackModel : ActionModelBase
{


    public MeleeAttackModel(IAttackHandler attackService)
    {
        _attackRange = 2.0f;
        _scopeOfEffect = 1.0f;

        _attackService = attackService; 
    }
    public override void DoAction()
    {
        DebugUtility.Log("MeleeAttackModel DoAction");
        _attackService.ExecuteAttack(_contModel.CharacterPos, _scopeOfEffect, 10);
    }
}

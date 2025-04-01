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
        //攻撃処理
    }
}
using System;

public class MeleeAttackAction : CharacterActionBase
{
    public MeleeAttackAction()
    {
        _attackRange = 2.0f;
        _scopeOfEffect = 1.0f;
    }
    public override void DoAction()
    {
        //攻撃処理
    }
}
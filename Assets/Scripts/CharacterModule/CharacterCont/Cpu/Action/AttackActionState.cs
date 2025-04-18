using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackActionState : FactionStateBase
{
    private CpuActionControllerModel _cpuAction;

    public AttackActionState(CpuActionControllerModel cpuAction, AllCharacterStatus allCharacter, IChangeActionRequest request)
    {
        _cpuAction = cpuAction;
        _allCharacterStatus = allCharacter;
        _request = request;
    }

    public override void EnterState()
    {
        float targetDistance = Vector3.Distance(_allCharacterStatus.MyCharacterStatus.NowPosition, _allCharacterStatus.EnemyCharacterStatus[0].NowPosition);

        foreach (ActionModelBase action in _cpuAction.ActionList)
        {
            if (action.AttackRange > targetDistance)
            {
                List<Collider> targets = action.CheckActionTarget();
                action.DoAction(targets);
                break;
            }
        }

        _request.NoticeEnd();

        
    }
    public override void UpdateState()
    {
        // 攻撃の実行
        Console.WriteLine("攻撃を実行しています。");
    }
    public override void ExitState()
    {
        // 攻撃の後処理をする
        Console.WriteLine("攻撃の後処理をしています。");
    }
}

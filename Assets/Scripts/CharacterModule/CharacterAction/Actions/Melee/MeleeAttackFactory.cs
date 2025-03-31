using System;
using UnityEngine;

public class MeleeAttackFactory : ActionFactoryBase
{
    [SerializeField] 
    private MeleeAttackView _view;

    public override ActionMVPData CreateAction()
    {
        MeleeAttackModel model = new MeleeAttackModel();

        new ActionPresenter(model, _view).Bind();

        return new ActionMVPData(model, _view);
    }
}
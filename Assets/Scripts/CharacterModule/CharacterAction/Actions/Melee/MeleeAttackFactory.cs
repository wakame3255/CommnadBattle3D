using System;
using UnityEngine;

public class MeleeAttackFactory : ActionFactoryBase
{
    [SerializeField] 
    private MeleeAttackView _view;

    public override ActionMVPData CreateAction(Transform parent)
    {
        MeleeAttackView view = Instantiate(_view, parent);

        MeleeAttackModel model = new MeleeAttackModel();

         new ActionPresenter(model, view).Bind();

        return new ActionMVPData(model, view);
    }
}
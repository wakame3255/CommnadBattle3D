using System;
using UnityEngine;

public class MeleeAttackFactory : ActionFactoryBase
{
    [SerializeField] 
    private MeleeAttackView _view;

    public override ActionMVPData CreateAction(Transform parent, IAttackHandler attackService)
    {
        MeleeAttackView view = Instantiate(_view, parent);

        MeleeAttackModel model = new MeleeAttackModel(attackService);

         new ActionPresenter(model, view).Bind();

        return new ActionMVPData(model, view);
    }
}
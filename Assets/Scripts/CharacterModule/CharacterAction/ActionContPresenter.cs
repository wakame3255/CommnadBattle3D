using System;
using R3;

public class ActionContPresenter : IBinder
{
    private ActionContModel _model;

    private ActionContView _view;

    public ActionContPresenter(ActionContModel model, ActionContView view)
    {
        _model = model;
        _view = view;

        _model.Initialize();
        _view.Initialize();
    }

    public void Bind()
    {
        _model.RPCurrentAction.Subscribe(_view.SetAttackRange);
    }
}
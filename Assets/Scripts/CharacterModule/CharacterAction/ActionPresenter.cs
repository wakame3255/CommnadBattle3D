using System;

public class ActionPresenter : IBinder
{
    private ActionModel _model;

    private ActionView _view;

    public ActionPresenter(ActionModel model, ActionView view)
    {
        _model = model;
        _view = view;

        _model.Initialize();
        _view.Initialize();
    }

    public void Bind()
    {
        
    }
}
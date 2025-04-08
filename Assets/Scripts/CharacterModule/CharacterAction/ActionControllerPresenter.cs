using System;
using R3;

public class ActionControllerPresenter : IBinder
{
    private ActionControllerModelBase _model;

    private ActionControllerView _view;

    private ActionHighlightsView _actionHighlights;

    public ActionControllerPresenter(ActionControllerModelBase model, ActionControllerView view, ActionHighlightsView actionHighlights)
    {
        _model = model;
        _view = view;
        _actionHighlights = actionHighlights;

        _model.Initialize();
        _view.Initialize();
    }

    public void Bind()
    {
        _model.RPCurrentAction.Subscribe(_view.SetAttackRange);

        _model.RPTargets.Subscribe(_actionHighlights.InstanceHighlight);
    }
}

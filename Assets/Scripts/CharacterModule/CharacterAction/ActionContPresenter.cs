using System;
using R3;

public class ActionContPresenter : IBinder
{
    private ActionContModel _model;

    private ActionContView _view;

    private ActionHighlightsView _actionHighlights;

    public ActionContPresenter(ActionContModel model, ActionContView view, ActionHighlightsView actionHighlights)
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

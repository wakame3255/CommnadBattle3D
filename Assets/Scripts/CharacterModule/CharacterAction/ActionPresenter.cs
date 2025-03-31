using System;
using R3;

public class ActionPresenter : IBinder
{
    ActionModelBase _model;

    ActionViewBase _view;

    public ActionPresenter(ActionModelBase model, ActionViewBase view)
    {
        _model = model;
        _view = view;
    }

    public void Bind()
    {
      _view.ActionButton.OnClickAsObservable()
          .Subscribe(_ => _model.NoticeActionModel());
    }
}
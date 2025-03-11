using System;
using R3;

public class TurnControllerPresenter : IBinder
{
    private TurnControllerModel _model;
    private TurnControllerView _view;

    public TurnControllerPresenter(TurnControllerModel model, TurnControllerView view)
    {
        _model = model;
        _view = view;

        model.Initialize();
        view.Initialize();
    }

    public void Bind()
    {
       _model.CharacterStateHandlers.Subscribe(_view.UpdateView);
    }
}
using System;
using R3;

public class PlayerCharacterContPresenter : IBinder
{
    private IPlayerContModel _model;

    private PlayerCharacterContView _view;

    public PlayerCharacterContPresenter(IPlayerContModel model, PlayerCharacterContView view)
    {
        _model = model;

        _view = view;

        model.Initialize();
        view.Initialize();
    }

    public void Bind()
    {
       _model.RPCurrentState.Subscribe(_view.UpdateView);
    }
}
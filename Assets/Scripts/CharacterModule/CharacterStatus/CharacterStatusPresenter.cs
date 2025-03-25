using System;
using R3;

public class CharacterStatusPresenter : IBinder
{
    private CharacterStatusModel _model;
    private CharacterStatusView _view;

    public CharacterStatusPresenter(CharacterStatusModel model, CharacterStatusView view)
    {
        _model = model;
        _view = view;

        _model.Initialize();
        _view.Initialize();
    }

    public void Bind()
    {
        // モデルの残り移動距離をビューにバインド
        _model.TravelDistance.Subscribe(_view.SetTravelDistance);

        // モデルの行動コストをビューにバインド
        _model.RPActionCost.Subscribe(_view.SetActionCost);

        // モデルのキャラクターの状態をビューにバインド
        _model.RPCurrentState.Subscribe(_view.SetCharacterState);
    }

    
}
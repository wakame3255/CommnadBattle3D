using System;
using R3;

public class PlayerCharacterContPresenter : IBinder, IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

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
        //プレイヤー状態の通知イベントを購読
        _model.RPCurrentState
            .Subscribe(_view.UpdateView)
            .AddTo(_disposables);

        //ターン終了ボタンクリックイベントを購読
        _view.EndTurnButton.OnClickAsObservable()
            .Subscribe(_ => _model.NoticeEndTurn())
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
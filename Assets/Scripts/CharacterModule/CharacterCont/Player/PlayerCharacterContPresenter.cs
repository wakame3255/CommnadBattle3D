using System;
using R3;

/// <summary>
/// プレイヤーキャラクター制御のプレゼンター
/// プレイヤーの状態とUIの連携を管理
/// </summary>
public class PlayerCharacterContPresenter : IBinder, IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private IPlayerContModel _model;

    private PlayerCharacterContView _view;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="model">プレイヤー制御モデル</param>
    /// <param name="view">プレイヤー制御ビュー</param>
    public PlayerCharacterContPresenter(IPlayerContModel model, PlayerCharacterContView view)
    {
        _model = model;

        _view = view;

        model.Initialize();
        view.Initialize();
    }

    /// <summary>
    /// モデルとビューのバインド処理
    /// 状態の変更とボタンイベントを接続
    /// </summary>
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

    /// <summary>
    /// リソースの解放
    /// </summary>
    public void Dispose()
    {
        _disposables.Dispose();
    }
}
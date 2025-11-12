using System;
using R3;

/// <summary>
/// インゲームのプレゼンター
/// ゲーム状態のモデルとビューを接続
/// </summary>
public class InGamePresenter : IBinder ,IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private InGameModel _model;

    private InGameView _view;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="model">インゲームモデル</param>
    /// <param name="view">インゲームビュー</param>
    public InGamePresenter(InGameModel model, InGameView view)
    {
        _model = model;

        _view = view;

        model.Initialize();
        view.Initialize();     
    }

    /// <summary>
    /// モデルとビューのバインド処理
    /// ゲーム状態の変更をビューに反映
    /// </summary>
    public void Bind()
    {
        _model.CurrentGameState.Subscribe(_view.UpdateView).AddTo(_disposables);
    }

    /// <summary>
    /// リソースの解放
    /// </summary>
    public void Dispose()
    {
        _disposables.Dispose();
    }
}
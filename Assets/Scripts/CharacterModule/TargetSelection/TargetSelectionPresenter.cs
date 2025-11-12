using System;
using R3;

/// <summary>
/// ターゲット選択機能を管理するプレゼンター
/// ターゲットの選択とステータス表示を制御
/// </summary>
public class TargetSelectionPresenter : IBinder, IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private TargetSelectionModel _model = default;
    private TargetSelectionView _view = default;

    private SelectTargetStatusView _statusView = default;
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="model">ターゲット選択モデル</param>
    /// <param name="view">ターゲット選択ビュー</param>
    /// <param name="statusView">ステータス表示ビュー</param>
    public TargetSelectionPresenter(TargetSelectionModel model, TargetSelectionView view, SelectTargetStatusView statusView)
    {
        _model = model;
        _view = view;
        _statusView = statusView;
    }

    /// <summary>
    /// モデルとビューのバインド処理
    /// </summary>
    public void Bind()
    {
        _view.RPSelectedTarget.Subscribe(_model.CheckTarget).AddTo(_disposables);

        _model.RPCharacterStatus.Subscribe(_statusView.SetCharacterInfomation).AddTo(_disposables);
    }

    /// <summary>
    /// リソースの解放
    /// </summary>
    public void Dispose()
    {
        _disposables.Dispose();
    }
}

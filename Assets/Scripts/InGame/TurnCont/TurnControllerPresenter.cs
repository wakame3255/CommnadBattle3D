using System;
using R3;

/// <summary>
/// ターン制御のプレゼンター
/// ターン管理のモデルとビューを接続
/// </summary>
public class TurnControllerPresenter : IBinder
{
    private TurnControllerModel _model;
    private TurnControllerView _view;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="model">ターン制御モデル</param>
    /// <param name="view">ターン制御ビュー</param>
    public TurnControllerPresenter(TurnControllerModel model, TurnControllerView view)
    {
        _model = model;
        _view = view;

        model.Initialize();
        view.Initialize();
    }

    /// <summary>
    /// モデルとビューのバインド処理
    /// キャラクターリストの変更をビューに反映
    /// </summary>
    public void Bind()
    {
       _model.CharacterStateHandlers.Subscribe(_view.UpdateView);
    }
}
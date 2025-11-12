using System;
using R3;

/// <summary>
/// アクション機能のプレゼンター
/// アクションモデルとビューを接続し、ボタンクリックイベントをバインド
/// </summary>
public class ActionPresenter : IBinder
{
    ActionModelBase _model;

    ActionViewBase _view;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="model">アクションモデル</param>
    /// <param name="view">アクションビュー</param>
    public ActionPresenter(ActionModelBase model, ActionViewBase view)
    {
        _model = model;
        _view = view;
    }

    /// <summary>
    /// モデルとビューのバインド処理
    /// アクションボタンのクリックイベントをモデルに通知
    /// </summary>
    public void Bind()
    {
      _view.ActionButton.OnClickAsObservable()
          .Subscribe(_ => _model.NoticeActionModel());
    }
}
using System;
using R3;

/// <summary>
/// アクション制御のプレゼンター
/// アクションの選択、範囲表示、対象ハイライト表示を管理
/// </summary>
public class ActionControllerPresenter : IBinder
{
    private ActionControllerModelBase _model;

    private ActionControllerView _view;

    private ActionHighlightsView _actionHighlights;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="model">アクション制御モデル</param>
    /// <param name="view">アクション制御ビュー</param>
    /// <param name="actionHighlights">アクションハイライト表示ビュー</param>
    public ActionControllerPresenter(ActionControllerModelBase model, ActionControllerView view, ActionHighlightsView actionHighlights)
    {
        _model = model;
        _view = view;
        _actionHighlights = actionHighlights;

        _model.Initialize();
        _view.Initialize();
    }

    /// <summary>
    /// モデルとビューのバインド処理
    /// アクション範囲と対象のハイライト表示を設定
    /// </summary>
    public void Bind()
    {
        _model.RPCurrentAction.Subscribe(_view.SetAttackRange);

        _model.RPTargets.Subscribe(_actionHighlights.InstanceHighlight);
    }
}

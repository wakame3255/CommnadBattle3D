using System;
using R3;

/// <summary>
/// キャラクターのステータス表示を管理するプレゼンター
/// HP、移動距離、行動コストなどをモデルからビューへバインド
/// </summary>
public class CharacterStatusPresenter : IBinder
{
    private CharacterStatusModel _model;
    private CharacterStatusView _view;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="model">キャラクターステータスモデル</param>
    /// <param name="view">キャラクターステータスビュー</param>
    public CharacterStatusPresenter(CharacterStatusModel model, CharacterStatusView view)
    {
        _model = model;
        _view = view;

        _model.Initialize();
        _view.Initialize();
    }

    /// <summary>
    /// モデルとビューのバインド処理
    /// 各種ステータスをビューに反映
    /// </summary>
    public void Bind()
    {
        // モデルの残り移動距離をビューにバインド
        _model.TravelDistance.Subscribe(_view.SetTravelDistance);

        // モデルの行動コストをビューにバインド
        _model.RPActionCost.Subscribe(_view.SetActionCost);

        // モデルのキャラクターの状態をビューにバインド
        _model.RPCurrentState.Subscribe(_view.SetCharacterState);

        // モデルのキャラクターのHPをビューにバインド
        _model.RPHealth.Subscribe(_view.SetHealth);
    }


}
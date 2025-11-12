using System;
using VContainer;
using VContainer.Unity;

/// <summary>
/// プレゼンターの初期化を管理するクラス
/// ゲーム開始時に各プレゼンターのバインド処理を実行
/// </summary>
public class PresenterStartUp : IStartable
{
    private TurnControllerPresenter _turnControllerPresenter;

    private InGamePresenter _inGamePresenter;

    private PlayerCharacterContPresenter _playerCharacterContPresenter;

    /// <summary>
    /// コンストラクタ
    /// 各プレゼンターを依存性注入で受け取る
    /// </summary>
    /// <param name="turnController">ターン制御プレゼンター</param>
    /// <param name="inGamePresenter">インゲームプレゼンター</param>
    /// <param name="playerCharacter">プレイヤーキャラクター制御プレゼンター</param>
    public PresenterStartUp(TurnControllerPresenter turnController, InGamePresenter inGamePresenter, PlayerCharacterContPresenter playerCharacter)
    {
        _turnControllerPresenter = turnController;

        _inGamePresenter = inGamePresenter;

        _playerCharacterContPresenter = playerCharacter;
    }
    
    /// <summary>
    /// 起動時の初期化処理
    /// 各プレゼンターのバインド処理を実行
    /// </summary>
    public void Start()
    {
        //各プレゼンターのバインド処理
        _turnControllerPresenter.Bind();
       _inGamePresenter.Bind();
        _playerCharacterContPresenter.Bind();
    }
}
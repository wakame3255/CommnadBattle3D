using System;
using R3;

public class TurnControllerModel : IInitialize
{
    /// <summary>
    /// 変更依頼を送るためのインターフェース
    /// </summary>
    private IGameStateChanger _gameStateChenger;

    /// <summary>
    /// 現在のメインゲーム状態保持
    /// </summary>
    private GameState _currentGameState;

    /// <summary>
    /// ゲーム管理クラスへの依存注入
    /// </summary>
    /// <param name="gameStateChenger">ゲームステートクラス</param>
    public TurnControllerModel(IGameStateChanger gameStateChenger)
    {
        _gameStateChenger = gameStateChenger;

        Initialize();

        Bind();
    }

    public void Initialize()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// イベントの結び付け
    /// </summary>
    private void Bind()
    {
        //メインゲームステートの購読
        _gameStateChenger.CurrentGameState.Subscribe(ChangeGameState);
    }

    /// <summary>
    /// 保持しているゲームステート変更、ステートによっての処理を行う
    /// </summary>
    /// <param name="gameState">メインゲームステート</param>
    public void ChangeGameState(GameState gameState)
    {
        _currentGameState = gameState;
    }
}
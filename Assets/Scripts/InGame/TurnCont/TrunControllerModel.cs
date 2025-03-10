using System;
using R3;

public class TrunControllerModel : IInitialize
{
    /// <summary>
    /// 変更依頼を送るためのインターフェース
    /// </summary>
    private IGameStateChenger _gameStateChenger;

    /// <summary>
    /// 現在のメインゲーム状態保持
    /// </summary>
    private GameState _cullentGameState;

    /// <summary>
    /// ゲーム管理クラスへの依存注入
    /// </summary>
    /// <param name="gameStateChenger">ゲームステートクラス</param>
    public TrunControllerModel(IGameStateChenger gameStateChenger)
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
        _gameStateChenger.CurrentGameState.Subscribe(ChengeGameState);
    }

    /// <summary>
    /// 保持しているゲームステート変更、ステートによっての処理を行う
    /// </summary>
    /// <param name="gameState">メインゲームステート</param>
    public void ChengeGameState(GameState gameState)
    {
        _cullentGameState = gameState;
    }
}
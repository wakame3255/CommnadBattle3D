using System;
using R3;

public interface IGameStateChanger
{
    /// <summary>
    /// 現在のメインゲーム状態公開保持
    /// </summary>
    public ReadOnlyReactiveProperty<GameState> CurrentGameState { get; }

    /// <summary>
    /// メインゲーム状態変更依頼
    /// </summary>
    /// <param name="gameState">移行したいステート</param>
    public void ChangeGameState(GameState gameState);
}
using System;
using R3;

public enum GameState
{
    Ready,
    Play,
    Pause,
    GameOver
}

public class InGameModel : IInitialize, IGameStateChanger
{

    //メインゲームの状態保持
    private ReactiveProperty<GameState> _currentGameState = new ReactiveProperty<GameState>();

    //メインゲームの状態公開保持
    public ReadOnlyReactiveProperty<GameState> CurrentGameState { get => _currentGameState; }

    public void Initialize()
    {
        _currentGameState.Value = GameState.Ready;
    }

    public void ChangeGameState(GameState gameState)
    {
        _currentGameState.Value = gameState;
    }
}
using System;
using R3;

public enum GameState
{
    Ready,
    Play,
    Pause,
    GameOver
}

public class InGameModel : IInitialize
{
    private ReactiveProperty<GameState> _currentGameState = new ReactiveProperty<GameState>();
    public ReadOnlyReactiveProperty<GameState> CurrentGameState { get => _currentGameState; }

    public void Initialize()
    {
        _currentGameState.Value = GameState.Ready;
    }
}
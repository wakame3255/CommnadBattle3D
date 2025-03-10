using System;
using R3;

public enum GameState
{
    Ready,
    Play,
    Pause,
    GameOver
}

public class InGameModel
{
    private ReactiveProperty<GameState> _gameState = new ReactiveProperty<GameState>();
    public ReadOnlyReactiveProperty<GameState> GameState { get => _gameState; }
}
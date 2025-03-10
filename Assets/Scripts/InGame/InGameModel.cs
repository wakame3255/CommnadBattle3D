using System;
using R3;

public enum GameState
{
    Ready,
    Play,
    Pause,
    GameOver
}

public class InGameModel : IInitialize, IGameStateChenger
{

    //���C���Q�[���̏�ԕێ�
    private ReactiveProperty<GameState> _currentGameState = new ReactiveProperty<GameState>();

    //���C���Q�[���̏�Ԍ��J�ێ�
    public ReadOnlyReactiveProperty<GameState> CurrentGameState { get => _currentGameState; }

    public void Initialize()
    {
        _currentGameState.Value = GameState.Ready;
    }

    public void ChengeGameState(GameState gameState)
    {
        _currentGameState.Value = gameState;
    }
}
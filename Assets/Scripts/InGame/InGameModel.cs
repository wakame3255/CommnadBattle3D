using System;
using R3;
using VContainer;

public enum GameState
{
    Ready,
    Play,
    Pause,
    GameOver,
    Clear,
}

public class InGameModel : IInitialize, IGameStateChanger
{
    private SceneChanger _sceneChanger;

    [Inject]
    public InGameModel(SceneChanger sceneChanger)
    {
        _sceneChanger = sceneChanger;
    }
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

        switch (gameState)
        {
            case GameState.Ready:
                break;
            case GameState.Play:
                break;
            case GameState.Pause:
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.Clear:
                Clear();
                break;
            default:
                break;
        }
    }

    public void Clear()
    {
       _sceneChanger.ChangeScene(ScenesNames.ClearScene);
    }
    public void GameOver()
    {
        _sceneChanger.ChangeScene(ScenesNames.GameOverScene);
    }
}

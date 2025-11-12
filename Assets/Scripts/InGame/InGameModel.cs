using System;
using R3;

/// <summary>
/// インゲーム状態を表す列挙型
/// </summary>
public enum GameState
{
    /// <summary>準備中</summary>
    Ready,
    /// <summary>プレイ中</summary>
    Play,
    /// <summary>一時停止</summary>
    Pause,
    /// <summary>ゲームオーバー</summary>
    GameOver
}

/// <summary>
/// インゲームのモデルクラス
/// ゲーム状態を管理
/// </summary>
public class InGameModel : IInitialize, IGameStateChanger
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public InGameModel()
    {
        
    }
    //インゲームの状態保持
    private ReactiveProperty<GameState> _currentGameState = new ReactiveProperty<GameState>();

    //インゲームの状態公開保持
    /// <summary>
    /// 現在のゲーム状態を公開するリアクティブプロパティ
    /// </summary>
    public ReadOnlyReactiveProperty<GameState> CurrentGameState { get => _currentGameState; }

    /// <summary>
    /// 初期化処理
    /// ゲーム状態をReadyに設定
    /// </summary>
    public void Initialize()
    {
        _currentGameState.Value = GameState.Ready;
    }

    /// <summary>
    /// ゲーム状態を変更
    /// </summary>
    /// <param name="gameState">変更後のゲーム状態</param>
    public void ChangeGameState(GameState gameState)
    {
        _currentGameState.Value = gameState;
    }
}
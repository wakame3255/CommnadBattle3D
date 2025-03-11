using System;
using R3;

public class TurnControllerModel : IInitialize
{
    /// <summary>
    /// 変更依頼を送るためのインターフェース
    /// </summary>
    private IGameStateChanger _gameStateChanger;

    /// <summary>
    /// キャラクター生成クラス
    /// </summary>
    private ICharacterGenerator _characterGenerator;

    /// <summary>
    /// 現在のメインゲーム状態保持
    /// </summary>
    private GameState _currentGameState;

    /// <summary>
    /// キャラクターイベント
    /// </summary>
    private ReactiveProperty<ICharacterStateHandler[]> _characterStateHandlers;

    /// <summary>
    /// キャラクターステートハンドラーのイベント公開
    /// </summary>
    public ReadOnlyReactiveProperty<ICharacterStateHandler[]> CharacterStateHandlers { get => _characterStateHandlers; }

    /// <summary>
    /// ゲーム管理クラスへの依存注入
    /// </summary>
    /// <param name="gameStateChenger">ゲームステートクラス</param>
    public TurnControllerModel(IGameStateChanger gameStateChenger, ICharacterGenerator characterGenerator)
    {
        _gameStateChanger = gameStateChenger;
        _characterGenerator = characterGenerator;
    }

    public void Initialize()
    {
        _characterStateHandlers = new ReactiveProperty<ICharacterStateHandler[]>();

        GenerateCharacter();

        Bind();
    }

    /// <summary>
    /// イベントの結び付け
    /// </summary>
    private void Bind()
    {
        //メインゲームステートの購読
        _gameStateChanger.CurrentGameState.Subscribe(ChangeGameState);
    }

    /// <summary>
    /// 保持しているゲームステート変更、ステートによっての処理を行う
    /// </summary>
    /// <param name="gameState">メインゲームステート</param>
    public void ChangeGameState(GameState gameState)
    {
        _currentGameState = gameState;

        switch (_currentGameState)
        {
            case GameState.Ready:
                break;
            case GameState.Play:
                break;
            case GameState.Pause:
                break;
            case GameState.GameOver:
                break;
        }
    }

    /// <summary>
    /// キャラクター生成
    /// </summary>
    private void GenerateCharacter()
    {
        _characterStateHandlers.Value = _characterGenerator.GenerateCharacter();
    }
}
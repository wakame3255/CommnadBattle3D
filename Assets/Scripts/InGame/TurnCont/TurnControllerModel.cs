using R3;
using System;
using System.Collections.Generic;

public class TurnControllerModel : IInitialize, IDisposable
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
    /// 現在のキャラクター保持
    /// </summary>
    private ICharacterStateController _currentCharacter;

    /// <summary>
    /// キャラクターイベント
    /// </summary>
    private ReactiveProperty<List<ICharacterStateController>> _characterStateHandlers;

    /// <summary>
    /// キャラクターステートハンドラーのイベント公開
    /// </summary>
    public ReadOnlyReactiveProperty<List<ICharacterStateController>> CharacterStateHandlers { get => _characterStateHandlers; }

    private readonly CompositeDisposable _disposable = new CompositeDisposable();

    /// <summary>
    /// 購読を破棄するための変数
    /// </summary>
    private IDisposable _subscriptionState;

    private int _enemyCount = 0;
    private int _playerCount = 0;

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
        _characterStateHandlers = new ReactiveProperty<List<ICharacterStateController>>();

        GenerateCharacter();

        Bind();

        ChangeGameState(GameState.Play);
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
                StartTurn();
                break;
            case GameState.Pause:
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void Dispose()
    {
        _disposable.Dispose();
        _subscriptionState = null;
    }

    /// <summary>
    /// ターン開始
    /// </summary>
    private void StartTurn()
    {
        SubscribeCharacterState(_characterStateHandlers.Value[0]);
    }

    /// <summary>
    /// 行動するキャラクターの変更
    /// </summary>
    private void ChangeCharacterState(CharacterState characterState)
    {
        if (characterState == CharacterState.End)
        {
            //現在のキャラクターの購読を破棄
            _subscriptionState?.Dispose();

            //現在のキャラクターStay状態に
            _currentCharacter.ChangeCharacterState(CharacterState.Stay);

            //次のキャラクターのインデックスを取得
            int nextCharacterIndex = GetNextCharacterStateIndex(_currentCharacter);

            //次のキャラクターを購読
            SubscribeCharacterState(_characterStateHandlers.Value[nextCharacterIndex]);
        }
    }

    /// <summary>
    /// 行動するキャラクターステートの購読
    /// </summary>
    /// <param name="characterState"></param>
    private void SubscribeCharacterState(ICharacterStateController characterState)
    {
        _currentCharacter = characterState;

        _currentCharacter.ChangeCharacterState(CharacterState.Move);

        //キャラクターの状態を購読
        _subscriptionState = _currentCharacter.RPCurrentState
            .Subscribe(ChangeCharacterState)
            .AddTo(_disposable);
    }

    /// <summary>
    /// 次のキャラクターのステートインデックスを取得
    /// </summary>
    /// <param name="characterState">現在のキャラクター</param>
    /// <returns>次のキャラのインデックス</returns>
    private int GetNextCharacterStateIndex(ICharacterStateController characterState)
    {
        //今保持しているキャラクターのインデックスを取得
        int currentIndex = _characterStateHandlers.Value.IndexOf(characterState);

        //最後のキャラクターだったら最初のキャラクターに
        if (currentIndex + 1 >= _characterStateHandlers.Value.Count)
        {
            return 0;
        }

        return currentIndex + 1;
    }

    /// <summary>
    /// キャラクター生成
    /// </summary>
    private void GenerateCharacter()
    {
        _characterStateHandlers.Value = _characterGenerator.GenerateCharacter();

        //最初のキャラクターをセット
        _currentCharacter = _characterStateHandlers.Value[0];

        foreach (ICharacterStateController character in _characterStateHandlers.Value)
        {
            DebugUtility.Log(character.ToString());
            switch (character)
            {
                case PlayerCharacterControllerModel player:
                    character.RPCurrentState
                .Subscribe(state =>
                {
                    if (state == CharacterState.Dead)
                    {
                        DeathPlayer();
                    }
                })
                .AddTo(_disposable);
                    _playerCount++;
                    break;
                case CpuCharacterControllerModel cpu:
                    
                    character.RPCurrentState
                        .Subscribe(state =>
                        {
                            if (state == CharacterState.Dead)
                            {
                                DeathEnemy();
                            }
                        })
                        .AddTo(_disposable);
                    _enemyCount++;
                    break;
            }
        }
    }

    private void DeathEnemy()
    {
        _enemyCount--;
        CheckDeathCharacter();
    }
    private void DeathPlayer()
    {
        _playerCount--;
        CheckDeathCharacter();
    }
    private void CheckDeathCharacter()
    {
        if (_enemyCount <= 0)
        {
            //プレイヤーの勝利
            _gameStateChanger.ChangeGameState(GameState.Clear);
        }
        else if (_playerCount <= 0)
        {
            //CPUの勝利
            _gameStateChanger.ChangeGameState(GameState.GameOver);
        }
    }
}

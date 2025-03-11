using System;
using R3;

public class TurnControllerModel : IInitialize
{
    /// <summary>
    /// �ύX�˗��𑗂邽�߂̃C���^�[�t�F�[�X
    /// </summary>
    private IGameStateChanger _gameStateChanger;

    /// <summary>
    /// �L�����N�^�[�����N���X
    /// </summary>
    private ICharacterGenerator _characterGenerator;

    /// <summary>
    /// ���݂̃��C���Q�[����ԕێ�
    /// </summary>
    private GameState _currentGameState;

    /// <summary>
    /// �L�����N�^�[�C�x���g
    /// </summary>
    private ReactiveProperty<ICharacterStateHandler[]> _characterStateHandlers;

    /// <summary>
    /// �L�����N�^�[�X�e�[�g�n���h���[�̃C�x���g���J
    /// </summary>
    public ReadOnlyReactiveProperty<ICharacterStateHandler[]> CharacterStateHandlers { get => _characterStateHandlers; }

    /// <summary>
    /// �Q�[���Ǘ��N���X�ւ̈ˑ�����
    /// </summary>
    /// <param name="gameStateChenger">�Q�[���X�e�[�g�N���X</param>
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
    /// �C�x���g�̌��ѕt��
    /// </summary>
    private void Bind()
    {
        //���C���Q�[���X�e�[�g�̍w��
        _gameStateChanger.CurrentGameState.Subscribe(ChangeGameState);
    }

    /// <summary>
    /// �ێ����Ă���Q�[���X�e�[�g�ύX�A�X�e�[�g�ɂ���Ă̏������s��
    /// </summary>
    /// <param name="gameState">���C���Q�[���X�e�[�g</param>
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
    /// �L�����N�^�[����
    /// </summary>
    private void GenerateCharacter()
    {
        _characterStateHandlers.Value = _characterGenerator.GenerateCharacter();
    }
}
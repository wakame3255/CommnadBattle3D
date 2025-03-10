using System;
using R3;

public class TurnControllerModel : IInitialize
{
    /// <summary>
    /// �ύX�˗��𑗂邽�߂̃C���^�[�t�F�[�X
    /// </summary>
    private IGameStateChanger _gameStateChenger;

    /// <summary>
    /// ���݂̃��C���Q�[����ԕێ�
    /// </summary>
    private GameState _currentGameState;

    /// <summary>
    /// �Q�[���Ǘ��N���X�ւ̈ˑ�����
    /// </summary>
    /// <param name="gameStateChenger">�Q�[���X�e�[�g�N���X</param>
    public TurnControllerModel(IGameStateChanger gameStateChenger)
    {
        _gameStateChenger = gameStateChenger;

        Initialize();

        Bind();
    }

    public void Initialize()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// �C�x���g�̌��ѕt��
    /// </summary>
    private void Bind()
    {
        //���C���Q�[���X�e�[�g�̍w��
        _gameStateChenger.CurrentGameState.Subscribe(ChangeGameState);
    }

    /// <summary>
    /// �ێ����Ă���Q�[���X�e�[�g�ύX�A�X�e�[�g�ɂ���Ă̏������s��
    /// </summary>
    /// <param name="gameState">���C���Q�[���X�e�[�g</param>
    public void ChangeGameState(GameState gameState)
    {
        _currentGameState = gameState;
    }
}
using System;
using R3;

public interface IGameStateChenger
{
    /// <summary>
    /// ���݂̃��C���Q�[����Ԍ��J�ێ�
    /// </summary>
    public ReadOnlyReactiveProperty<GameState> CurrentGameState { get; }

    /// <summary>
    /// ���C���Q�[����ԕύX�˗�
    /// </summary>
    /// <param name="gameState">�ڍs�������X�e�[�g</param>
    public void ChengeGameState(GameState gameState);
}
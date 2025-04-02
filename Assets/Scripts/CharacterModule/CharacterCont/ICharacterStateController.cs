using R3;

public enum CharacterState
{
    Stay,
    Move,
    End,
}

public interface ICharacterStateController
{
    /// <summary>
    /// キャラクターの現在の状態
    /// </summary>
    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get; }

    /// <summary>
    /// キャラクターの状態を変更する
    /// </summary>
    /// <param name="characterState">変更したいステート</param>
    public void ChangeCharacterState(CharacterState characterState);
}

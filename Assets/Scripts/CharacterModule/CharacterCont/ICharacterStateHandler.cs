using R3;

public enum CharacterState
{
    Stay,
    Move,
    End,
}

public interface ICharacterStateHandler
{
    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get; }

    public void ChangeCharacterState(CharacterState characterState);
}
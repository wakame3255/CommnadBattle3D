using System;
using R3;

public enum CharacterState
{
    Idle,
    Move,
    Attack,
    Damage,
    Dead
}

public interface ICharacterStateHandler
{
    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get; }

    public void ChangeCharacterState(CharacterState characterState);
}
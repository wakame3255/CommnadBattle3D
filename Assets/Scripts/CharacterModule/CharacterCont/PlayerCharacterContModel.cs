using System;
using R3;

public class PlayerCharacterContModel : ICharacterStateHandler, IPlayerContModel
{
    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get; }

    public PlayerCharacterContModel()
    {
        RPCurrentState = new ReactiveProperty<CharacterState>(CharacterState.Idle);
    }

    public void ChangeCharacterState(CharacterState characterState)
    {

    }
}
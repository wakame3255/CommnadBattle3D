using System;
using R3;

public class CpuCharacterContModel : ICharacterStateHandler
{
    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get; }

    public void ChangeCharacterState(CharacterState characterState)
    {
    }
}
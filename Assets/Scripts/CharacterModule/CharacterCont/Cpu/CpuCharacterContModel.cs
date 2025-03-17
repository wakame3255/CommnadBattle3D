using System;
using R3;

public class CpuCharacterContModel : ICharacterStateHandler
{
    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get; }

    public CpuCharacterContModel()
    {
        RPCurrentState = new ReactiveProperty<CharacterState>(CharacterState.Stay);
    }

    public void ChangeCharacterState(CharacterState characterState)
    {
        switch (characterState)
        {
            case CharacterState.Stay:
                break;
            case CharacterState.Move:
                break;
            case CharacterState.End:
                break;
        }
    }
}
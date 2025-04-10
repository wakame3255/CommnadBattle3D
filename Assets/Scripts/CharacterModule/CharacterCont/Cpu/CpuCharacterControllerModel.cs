using System;
using R3;

public class CpuCharacterControllerModel : ICharacterStateController
{
    private ReactiveProperty<CharacterState> _currentState;
    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get => _currentState; }

    private FactionStateController _factionStateController;

    public CpuCharacterControllerModel()
    {
        _currentState = new ReactiveProperty<CharacterState>(CharacterState.Stay);
    }

    public void ChangeCharacterState(CharacterState characterState)
    {
        _currentState.Value = characterState;
    }
}

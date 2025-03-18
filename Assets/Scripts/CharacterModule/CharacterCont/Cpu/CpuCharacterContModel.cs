using System;
using R3;

public class CpuCharacterContModel : ICharacterStateHandler
{
    private ReactiveProperty<CharacterState> _currentState;
    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get => _currentState; }

    private CpuLogicModel _cpuLogicModel;

    public CpuCharacterContModel()
    {
        _currentState = new ReactiveProperty<CharacterState>(CharacterState.Stay);

        _cpuLogicModel = new CpuLogicModel(this);
    }

    public void ChangeCharacterState(CharacterState characterState)
    {
        _currentState.Value = characterState;
    }
}
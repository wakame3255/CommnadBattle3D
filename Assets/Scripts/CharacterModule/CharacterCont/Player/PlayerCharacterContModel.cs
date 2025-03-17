using System;
using R3;

public class PlayerCharacterContModel : ICharacterStateHandler, IPlayerContModel
{
    private ReactiveProperty<CharacterState> _rPCurrentState;

    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get => _rPCurrentState; }

    public PlayerCharacterContModel()
    {
        _rPCurrentState = new ReactiveProperty<CharacterState>(CharacterState.Stay);
    }

    public void Initialize()
    {
        
    }

    public void NoticeEndTurn()
    {
        ChangeCharacterState(CharacterState.End);
    }

    public void ChangeCharacterState(CharacterState characterState)
    {
        _rPCurrentState.Value = characterState;
    }
}
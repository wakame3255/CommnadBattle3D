using R3;
using System;

public interface IPlayerContModel
{
    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get; }
}
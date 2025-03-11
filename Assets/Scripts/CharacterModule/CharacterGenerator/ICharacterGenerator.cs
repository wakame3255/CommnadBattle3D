using System;

public interface ICharacterGenerator
{
    public ICharacterStateHandler[] GenerateCharacter();
}
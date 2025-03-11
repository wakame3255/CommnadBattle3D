using System;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour, ICharacterGenerator
{

    public CharacterGenerator()
    {

    }

    public ICharacterStateHandler[] GenerateCharacter()
    {
        return new ICharacterStateHandler[2];
    }
}
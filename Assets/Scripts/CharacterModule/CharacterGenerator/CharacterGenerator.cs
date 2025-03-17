using System;
using UnityEngine;
using VContainer;

public class CharacterGenerator : MonoBehaviour, ICharacterGenerator
{

    [SerializeField] 
    private GameObject characterPrefab;

    public ICharacterStateHandler[] GenerateCharacter()
    {
        return new ICharacterStateHandler[2];
    }
}
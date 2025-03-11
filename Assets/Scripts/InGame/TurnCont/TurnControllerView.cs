using System;
using UnityEngine;
using UnityEngine.UI;

public class TurnControllerView : MonoBehaviour, IInitialize
{
    [SerializeField]
    private Button _CharacterButton = default;

    public void Initialize()
    {
       
    }

    public void UpdateView(ICharacterStateHandler[] characterStateHandler)
    {
        for (int i = 0; i < characterStateHandler.Length; i++)
        {
            Instantiate(_CharacterButton, this.transform);
        }
    }
}
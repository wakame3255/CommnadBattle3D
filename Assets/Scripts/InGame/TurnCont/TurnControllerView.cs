using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnControllerView : MonoBehaviour, IInitialize
{
    [SerializeField]
    private Button _CharacterButton = default;

    public void Initialize()
    {
       
    }

    public void UpdateView(List<ICharacterStateHandler> characterStateHandler)
    {
        for (int i = 0; i < characterStateHandler.Count; i++)
        {
            Instantiate(_CharacterButton, this.transform);
        }
    }
}
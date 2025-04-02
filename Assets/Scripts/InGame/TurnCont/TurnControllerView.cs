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

    public void UpdateView(List<ICharacterStateController> characterStateHandlers)
    {
        for (int i = 0; i < characterStateHandlers.Count; i++)
        {
            Instantiate(_CharacterButton, this.transform);
        }
    }
}
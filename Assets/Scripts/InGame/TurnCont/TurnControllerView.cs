using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;
using UnityEngine.UI;

public class TurnControllerView : MonoBehaviour, IInitialize
{
    [SerializeField, Required]
    private Button _CharacterButton = default;

    public void Initialize()
    {
       
    }

    public void UpdateView(List<ICharacterStateController> characterStateHandlers)
    {
        //for (int i = 0; i < characterStateHandlers.Count; i++)
        //{
        //    Instantiate(_CharacterButton, this.transform);
        //}
    }
}

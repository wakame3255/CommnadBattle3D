using System;
using UnityEngine;
using UnityEngine.UI;
using R3;

public abstract class ActionViewBase : MonoBehaviour, IInitialize
{
    [SerializeField] 
    protected Button _actionButton;

    public Button ActionButton => _actionButton;

    public void Initialize()
    {
        throw new NotImplementedException();
    }
}
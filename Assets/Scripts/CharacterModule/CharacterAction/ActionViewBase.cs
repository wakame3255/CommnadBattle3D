using System;
using UnityEngine;
using UnityEngine.UI;
using R3;

public abstract class ActionViewBase : MonoBehaviour, IInitialize
{
    [SerializeField] 
    protected Button _actionButton;

    public Button ActionButton => _actionButton;

    protected ActionControllerView _actionContView;

    public void Initialize()
    {
        
    }

    public void SetActionContView(ActionControllerView actionCont)
    {
        _actionContView = actionCont;
    }
}
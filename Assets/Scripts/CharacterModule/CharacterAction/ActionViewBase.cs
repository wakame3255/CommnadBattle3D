using System;
using UnityEngine;
using UnityEngine.UI;
using R3;

public abstract class ActionViewBase : MonoBehaviour, IInitialize
{
    [SerializeField] 
    protected Button _actionButton;

    public Button ActionButton => _actionButton;

    protected ActionContView _actionContView;

    public void Initialize()
    {
        
    }

    public void SetActionContView(ActionContView actionCont)
    {
        _actionContView = actionCont;
    }
}
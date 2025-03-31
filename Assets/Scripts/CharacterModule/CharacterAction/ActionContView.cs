
using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionContView : MonoBehaviour, IInitialize
{
    private List<ActionViewBase> _viewBases = new List<ActionViewBase>();

    /// <summary>
    /// アクションビューの設定
    /// </summary>
    /// <param name="actionViews"></param>
    public void SetActionView(List<ActionViewBase> actionViews)
    {
        _viewBases = actionViews;
    }

    public void Initialize()
    {
        
    }
}
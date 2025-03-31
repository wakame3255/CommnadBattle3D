
using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionContView : MonoBehaviour, IInitialize
{
    private List<ActionViewBase> _viewBases = new List<ActionViewBase>();

    private GameObject _rangeObj;

    private void Start()
    {
        _rangeObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _rangeObj.transform.position = transform.position;
    }

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

    /// <summary>
    /// 攻撃範囲をviewに設定
    /// </summary>
    public void SetAttackRange(ActionModelBase actionModel)
    {
        if (_rangeObj == null)
        {
            return;
        }

        _rangeObj.transform.localScale = actionModel.AttackRange * Vector3.one;
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionControllerView : MonoBehaviour, IInitialize
{
    private List<ActionViewBase> _viewBases = new List<ActionViewBase>();

    private GameObject _rangeObj;

    private Button _actionButton;

    private void Start()
    {
        _rangeObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _rangeObj.transform.position = transform.position;

        _rangeObj.GetComponent<Collider>().enabled = false;
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
        _rangeObj.transform.position = transform.position;
        _rangeObj.transform.localScale = actionModel.AttackRange * Vector3.one;
    }
}

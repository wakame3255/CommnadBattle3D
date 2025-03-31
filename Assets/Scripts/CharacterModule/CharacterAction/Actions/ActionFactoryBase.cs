using System;
using UnityEngine;

public abstract class ActionFactoryBase : MonoBehaviour
{
    /// <summary>
    /// アクションのモデルとviewを返す
    /// </summary>
    /// <returns>modelとview</returns>
    public abstract ActionMVPData CreateAction(Transform parent);
}

public class ActionMVPData
{
    private ActionModelBase _model;
    private ActionViewBase _view;

    public ActionModelBase Model => _model;
    public ActionViewBase View => _view;

    public ActionMVPData(ActionModelBase model, ActionViewBase view)
    {
        _model = model;
        _view = view;
    }
}
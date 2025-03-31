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
    private CharacterActionBase _model;
    private ActionViewBase _view;

    public CharacterActionBase Model => _model;
    public ActionViewBase View => _view;

    public ActionMVPData(CharacterActionBase model, ActionViewBase view)
    {
        _model = model;
        _view = view;
    }
}
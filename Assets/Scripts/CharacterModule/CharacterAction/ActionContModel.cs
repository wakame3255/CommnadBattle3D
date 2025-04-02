using System;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class ActionContModel : IInitialize, ISetActionModel
{


    private ReactiveProperty<ActionModelBase>  _rPCurrentAction;

    public ReadOnlyReactiveProperty<ActionModelBase> RPCurrentAction => _rPCurrentAction;

    private Vector3 _characterPos = default;

    public Vector3 CharacterPos => _characterPos;

    IActionNotice _actionNotice;

    private List<ActionModelBase> _actionList = new List<ActionModelBase>();

    public ActionContModel(IActionNotice actionNotice, INoticePosition noticePosition, List<ActionModelBase> characterActions)
    {
        _actionNotice = actionNotice;
        _actionList = characterActions;

        //キャラクターの位置を購読
        noticePosition.RPTransformPosition.Subscribe(pos => SetCharacterPos(Vector3Extensions.ToUnityVector3(pos)));

        //依存注入
        foreach (ActionModelBase modelBase in _actionList)
        {
            modelBase.SetActionCont(this);
        }
    }

    public void Initialize()
    {
        _rPCurrentAction = new ReactiveProperty<ActionModelBase>();
    }

    public void SetActionModel(ActionModelBase characterAction)
    {
        _rPCurrentAction.Value = characterAction;
    }

    private void SetCharacterPos(Vector3 pos)
    {
        _characterPos = pos;
    }
}

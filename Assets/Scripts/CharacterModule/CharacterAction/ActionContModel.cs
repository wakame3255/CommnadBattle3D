using System;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class ActionContModel : IInitialize, ISetActionModel
{
    /// <summary>
    /// 現在選択している行動
    /// </summary>
    private ReactiveProperty<ActionModelBase>  _rPCurrentAction;
    public ReadOnlyReactiveProperty<ActionModelBase> RPCurrentAction => _rPCurrentAction;

    /// <summary>
    /// キャラクターの位置
    /// </summary>
    private Vector3 _characterPos = default;
    public Vector3 CharacterPos => _characterPos;

    /// <summary>
    /// キャラクターの派閥
    /// </summary>
    private IFactionMember _faction = default;
    public IFactionMember Faction => _faction;

    IActionNotice _actionNotice;

    private List<ActionModelBase> _actionList = new List<ActionModelBase>();

    public ActionContModel(CharacterStatusModel actionContModel, INoticePosition noticePosition, List<ActionModelBase> characterActions)
    {
        _actionNotice = actionContModel;
        _actionList = characterActions;
        _faction = actionContModel;

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

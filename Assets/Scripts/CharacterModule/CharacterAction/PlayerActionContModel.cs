using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
public class PlayerActionContModel : ActionContModelBase
{
    private TargetSelectionModel _targetSelectionModel;
    public PlayerActionContModel(CharacterStatusModel actionContModel, INoticePosition noticePosition, List<ActionModelBase> characterActions, TargetSelectionModel selectionModel)
    {
        _actionNotice = actionContModel;
        _actionList = characterActions;
        _faction = actionContModel;
        _targetSelectionModel = selectionModel;

        //キャラクターの位置を購読
        noticePosition.RPTransformPosition.Subscribe(pos => SetCharacterPos(Vector3Extensions.ToUnityVector3(pos)));
        //ターゲット選択の情報を購読
        selectionModel.RPSelectedTarget.Subscribe(SetSelectTargetl);

        //依存注入
        foreach (ActionModelBase modelBase in _actionList)
        {
            modelBase.SetActionCont(this);
        }
    }

    /// <summary>
    /// ターゲット選択の情報を挿入
    /// </summary>
    /// <param name="target"></param>
    private void SetSelectTargetl(Collider target)
    {
        List<Collider> targets = new List<Collider>();
        targets.Add(target);

        _rPTargets.Value = targets;
    }
}

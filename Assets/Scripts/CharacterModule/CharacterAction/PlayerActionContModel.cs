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
    /// ターゲット選択の情報を挿入し、情報の更新を行う
    /// </summary>
    /// <param name="target"></param>
    private void SetSelectTargetl(Collider selectTarget)
    {
        List<SelectionTargetData> targetData = new List<SelectionTargetData>();

        //選択されたターゲットのみハイライトの変更
        foreach (SelectionTargetData scopeTarget in _rPTargets.CurrentValue)
        {
            if (scopeTarget.Collider == selectTarget)
            {
                targetData.Add(new SelectionTargetData(scopeTarget.Collider, true));
            }
            else
            {
                targetData.Add(new SelectionTargetData(scopeTarget.Collider, false));
            }
        }

        _rPTargets.Value = targetData;

        DebugUtility.Log("助長オブ");
    }
}

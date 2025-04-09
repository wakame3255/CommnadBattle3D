using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
public class PlayerActionControllerModel : ActionControllerModelBase
{
    private Collider _cacheSelectTarget = default;

    public PlayerActionControllerModel(CharacterStatusModel actionContModel, INoticePosition noticePosition, List<ActionModelBase> characterActions, TargetSelectionModel selectionModel)
    {
        _actionNotice = actionContModel;
        _actionList = characterActions;
        _faction = actionContModel;

        //キャラクターの位置を購読
        noticePosition.RPTransformPosition.Subscribe(pos => SetCharacterPosition(Vector3Extensions.ToUnityVector3(pos)));
        //ターゲット選択の情報を購読
        selectionModel.RPSelectedTarget.Subscribe(SetSelectTarget);

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
    private void SetSelectTarget(Collider selectTarget)
    {
        if (_rPTargets.CurrentValue == null || selectTarget == null)
        {
            return;
        }

        if (!CheckAttackTarget(selectTarget))
        {
            ChangeTargetHighlight(selectTarget);
        }
    }

    /// <summary>
    /// すでに選択されたターゲットを選択したら攻撃を行う
    /// </summary>
    /// <param name="selectTarget"></param>
    /// <returns></returns>
    private bool CheckAttackTarget(Collider selectTarget)
    {
        //選択している敵を選んだか
        if (selectTarget != _cacheSelectTarget)
        {
            return false;
        }

        //コストが足りているのか
        if (_rPCurrentAction.Value.ActionCost <= _actionNotice.RPActionCost.CurrentValue)
        {
            List<Collider> attackTargets = new List<Collider>();
            attackTargets.Add(selectTarget);
            _rPCurrentAction.Value.DoAction(attackTargets);

            _actionNotice.NotifyUseActionCost(_rPCurrentAction.Value.ActionCost);

            return true;
        }
        else
        {
            DebugUtility.Log("コストが足りない");
            return false;
        }
    }

    /// <summary>
    /// 選択されたターゲットのハイライトを変更する
    /// </summary>
    /// <param name="selectTarget"></param>
    private void ChangeTargetHighlight(Collider selectTarget)
    {
        List<SelectionTargetData> targetData = new List<SelectionTargetData>();

        //選択されたターゲットのみハイライトの変更
        foreach (SelectionTargetData scopeTarget in _rPTargets.CurrentValue)
        {
            if (scopeTarget.Collider == selectTarget)
            {
                targetData.Add(new SelectionTargetData(scopeTarget.Collider, true));
                _cacheSelectTarget = scopeTarget.Collider;
            }
            else
            {
                targetData.Add(new SelectionTargetData(scopeTarget.Collider, false));
            }
        }

        _rPTargets.Value = targetData;

        DebugUtility.Log("再インスタンス生成は助長");
    }
}

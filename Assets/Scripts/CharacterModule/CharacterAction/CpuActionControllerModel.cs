using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

/// <summary>
/// CPUキャラクターのアクション制御モデル
/// CPUが実行可能なアクションを管理し、行動決定を行う
/// </summary>
public class CpuActionControllerModel : ActionControllerModelBase
{
    /// <summary>
    /// 利用可能なアクションのリスト
    /// </summary>
    public List<ActionModelBase> ActionList => _actionList;

    /// <summary>
    /// コンストラクタ
    /// CPUのアクション制御を初期化
    /// </summary>
    /// <param name="characterStatusModel">キャラクターステータスモデル</param>
    /// <param name="noticePosition">位置情報通知インターフェース</param>
    /// <param name="characterActions">実行可能なアクションのリスト</param>
    public CpuActionControllerModel(CharacterStatusModel characterStatusModel, INoticePosition noticePosition, List<ActionModelBase> characterActions)
    {
        _actionNotice = characterStatusModel;
        _actionList = characterActions;
        _faction = characterStatusModel;

        //キャラクターの位置を購読
        noticePosition.RPTransformPosition.Subscribe(pos => SetCharacterPosition(Vector3Extensions.ToUnityVector3(pos)));

        //依存注入
        foreach (ActionModelBase modelBase in _actionList)
        {
            modelBase.SetActionCont(this);
        }
    }

   
}

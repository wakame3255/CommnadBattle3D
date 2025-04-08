using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
public class CpuActionContModel : ActionContModelBase
{


    public CpuActionContModel(CharacterStatusModel actionContModel, INoticePosition noticePosition, List<ActionModelBase> characterActions)
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

   
}

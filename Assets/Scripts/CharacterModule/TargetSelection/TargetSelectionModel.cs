using System;
using UnityEngine;
using R3;
using System.Collections.Generic;

public class TargetSelectionModel
{
    private Dictionary<Collider, CharacterStatusModel> _characterStatusMap;

    private ReactiveProperty<CharacterStatusModel> _rPCharacterStatus;

    public ReadOnlyReactiveProperty<CharacterStatusModel> RPCharacterStatus => _rPCharacterStatus;

    public TargetSelectionModel()
    {
        _characterStatusMap = new Dictionary<Collider, CharacterStatusModel>();

        _rPCharacterStatus = new ReactiveProperty<CharacterStatusModel>();
    }

    /// <summary>
    /// キャラクターの状態を取得
    /// </summary>
    /// <param name="characterStatus"></param>
    /// <param name="collider"></param>
    public void AddCharacterStatus(CharacterStatusModel characterStatus, Collider collider)
    {
        _characterStatusMap.Add(collider, characterStatus);
    }

    /// <summary>
    /// ターゲットかどうかのチェック
    /// </summary>
    /// <param name="collider"></param>
    public void CheckTarget(Collider collider)
    {
        if (collider == null)
        {
            return;
        }
        
        if (!_characterStatusMap.TryGetValue(collider, out CharacterStatusModel characterStatus))
        {
            return;
        }

        //キャラクターの状態を取得
        _rPCharacterStatus.Value = characterStatus;
    }
}

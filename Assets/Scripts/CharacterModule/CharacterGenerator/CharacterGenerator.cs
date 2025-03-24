using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class CharacterGenerator : MonoBehaviour, ICharacterGenerator
{
    List<ICharacterStateHandler> _characterStateHandlers = new List<ICharacterStateHandler>();

    /// <summary>
    /// プレイヤーキャラクターのモデルを受け取る
    /// </summary>
    /// <param name="playerCharacterContModel">プレイヤー</param>
    [Inject]
    public void Construct(PlayerCharacterContModel playerCharacterContModel, CharacterStatusView characterStatus)
    {
        _characterStateHandlers.Add(playerCharacterContModel);

        //キャラクターステータスの生成
        CharacterStatusModel characterStatusModel = new CharacterStatusModel(playerCharacterContModel);
        new CharacterStatusPresenter(characterStatusModel, characterStatus);
    }

    public List<ICharacterStateHandler> GenerateCharacter()
    {
        _characterStateHandlers.Add(new CpuCharacterContModel());
        _characterStateHandlers.Add(new CpuCharacterContModel());
        _characterStateHandlers.Add(new CpuCharacterContModel());

        return _characterStateHandlers;
    }
}
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
    public void Construct(PlayerCharacterContModel playerCharacterContModel)
    {
        _characterStateHandlers.Add(playerCharacterContModel);
    }

    public List<ICharacterStateHandler> GenerateCharacter()
    {
        _characterStateHandlers.Add(new CpuCharacterContModel());

        return _characterStateHandlers;
    }
}
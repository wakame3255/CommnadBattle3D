using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;
using VContainer;

public class CharacterGenerator : MonoBehaviour, ICharacterGenerator
{
    [SerializeField, Required]
    private GameObject _characterPrefab = default;

    List<ICharacterStateHandler> _characterStateHandlers = new List<ICharacterStateHandler>();

    /// <summary>
    /// プレイヤーキャラクターのモデルを受け取る
    /// </summary>
    /// <param name="playerCharacterContModel">プレイヤー</param>
    [Inject]
    public void GeneratePlayer(PlayerCharacterContModel playerCharacterContModel, CharacterStatusView characterStatus, IInputInformation input, PathFind pathFind)
    {
        GameObject player = Instantiate(_characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        _characterStateHandlers.Add(playerCharacterContModel);

        //キャラクターステータスの生成
        CharacterStatusModel characterStatusModel = new CharacterStatusModel(playerCharacterContModel);
        new CharacterStatusPresenter(characterStatusModel, characterStatus);

        //プレイヤーのインプット情報依存注入
        MoveView playerMove = player.AddComponent<MoveView>();
        playerMove.SetInput(input);

        IPathAgenter path = new PathAgent(pathFind, player.transform);

        //プレイヤーの移動機能の生成
        MoveModel playerMoveModel = new MoveModel(path, characterStatusModel);
        new MovePresenter(playerMoveModel, playerMove);
    }

    public List<ICharacterStateHandler> GenerateCharacter()
    {
        _characterStateHandlers.Add(new CpuCharacterContModel());
        _characterStateHandlers.Add(new CpuCharacterContModel());
        _characterStateHandlers.Add(new CpuCharacterContModel());

        return _characterStateHandlers;
    }
}
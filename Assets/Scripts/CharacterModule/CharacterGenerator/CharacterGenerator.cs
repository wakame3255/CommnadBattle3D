using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;
using VContainer;

public class CharacterGenerator : MonoBehaviour, ICharacterGenerator
{
    [SerializeField, Required]
    private GameObject _characterPrefab = default;

    private List<ICharacterStateHandler> _characterStateHandlers = new List<ICharacterStateHandler>();

    /// <summary>
    /// 更新処理を行うハンドラ群
    /// </summary>
    private List<IUpdateHandler> _updateHandlers = new List<IUpdateHandler>();

    /// <summary>
    /// 攻撃計算を行うクラス
    /// </summary>
    private AttackCalculation _attackCalculation = new AttackCalculation();

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
        new CharacterStatusPresenter(characterStatusModel, characterStatus).Bind();

        //プレイヤーのインプット情報依存注入
        MoveView playerView = player.AddComponent<MoveView>();
        playerView.SetInput(input);

        IPathAgenter path = new PathAgent(pathFind, playerView.transform);

        //プレイヤーの移動機能の生成
        MoveModel playerMoveModel = new MoveModel(path, characterStatusModel);
        new MovePresenter(playerMoveModel, playerView).Bind();

        //キャラクターの情報注入
        _updateHandlers.Add(playerMoveModel);

        if (player.TryGetComponent<Collider>(out Collider collider))
        {
            _attackCalculation.AddDamageNotice(characterStatusModel, collider);
        }
    }

    [Obsolete("いずれは依存をなくしたい")]
    private void Update()
    {
        for (int i = 0; i < _updateHandlers.Count; i++)
        {
            _updateHandlers[i].Updateable();
        }
    }

    public List<ICharacterStateHandler> GenerateCharacter()
    {
        _characterStateHandlers.Add(new CpuCharacterContModel());
        _characterStateHandlers.Add(new CpuCharacterContModel());
        _characterStateHandlers.Add(new CpuCharacterContModel());

        return _characterStateHandlers;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using UnityEngine;
using VContainer;

public class CharacterGenerator : MonoBehaviour, ICharacterGenerator
{
    private int _characterCount = 0;

    [SerializeField, Required]
    private GameObject _characterPrefab = default;

    [SerializeField, Required, Obsolete]
    private MeleeAttackFactory _actionViewBasePrefab = default;

    private List<ICharacterStateController> _characterStateHandlers = new List<ICharacterStateController>();

    /// <summary>
    /// 更新処理を行うハンドラ群
    /// </summary>
    private List<IUpdateHandler> _updateHandlers = new List<IUpdateHandler>();

    /// <summary>
    /// 攻撃を行うクラス
    /// </summary>
    private AttackService _attackService = new AttackService();

    private TargetSelectionModel _targetSelectionModel = new TargetSelectionModel();

    /// <summary>
    /// プレイヤーキャラクターのモデルを受け取る
    /// </summary>
    /// <param name="playerCharacterContModel">プレイヤー</param>
    [Inject]
    public void GeneratePlayer(
        PlayerCharacterControllerModel playerCharacterContModel,
        CharacterStatusView characterStatus,
        IInputInformation input,
        PathFind pathFind,
        SelectTargetStatusView statusView,
        ActionHighlightsView highlightsView)
    {
        GameObject player = Instantiate(_characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        _characterStateHandlers.Add(playerCharacterContModel);

        //キャラクターステータスの生成
        CharacterStatusModel characterStatusModel = new CharacterStatusModel(playerCharacterContModel, Faction.Player);
        new CharacterStatusPresenter(characterStatusModel, characterStatus).Bind();

        //プレイヤーの移動機能の生成
        (MoveModel moveModel, MoveView moveView) = GetMoveSystem(player, pathFind, characterStatusModel);
        moveView.SetInput(input);

        //プレイヤーのターゲット選択機能の生成
        TargetSelectionView targetSelectionView = player.AddComponent<TargetSelectionView>();
        targetSelectionView.SetInputSelect(input);
        new TargetSelectionPresenter(_targetSelectionModel, targetSelectionView, statusView).Bind();

        //プレイヤーのアクション機能の生成
        (List<ActionModelBase> actionModels, List<ActionViewBase> actionViews) = GetActionModels();    
        ActionControllerModelBase actionContModel = new PlayerActionControllerModel(characterStatusModel, moveModel, actionModels, _targetSelectionModel);

        //プレイヤーのアクションビューの生成
        ActionControllerView actionContView = player.AddComponent<ActionControllerView>();
        actionContView.SetActionView(actionViews);
        new ActionControllerPresenter(actionContModel, actionContView, highlightsView).Bind();

        AttachCollider(player, characterStatusModel);
    }

    [Obsolete("いずれは依存をなくしたい")]
    private void Update()
    {
        for (int i = 0; i < _updateHandlers.Count; i++)
        {
            _updateHandlers[i].Updateable();
        }
    }

    public List<ICharacterStateController> GenerateCharacter()
    {

        for (int i = 0; i < 3; i++)
        {
            CpuCharacterControllerModel cpuCharacterContModel = new CpuCharacterControllerModel();

            _characterStateHandlers.Add(cpuCharacterContModel);

            SetupCpuCharacter(cpuCharacterContModel);
        }

        return _characterStateHandlers;
    }

    /// <summary>
    /// CPUキャラクターの生成
    /// </summary>
    /// <param name="characterState"></param>
    private void SetupCpuCharacter(ICharacterStateController characterState)
    {
        //敵の生成
        GameObject enemy = Instantiate(_characterPrefab, new Vector3(0, 0, _characterCount), Quaternion.identity);

        //キャラクターステータスの生成
        CharacterStatusModel characterStatusModel = new CharacterStatusModel(characterState, Faction.Enemy);

        AttachCollider(enemy, characterStatusModel);

        _characterCount += 2;
    }

    /// <summary>
    /// 移動システムの取得
    /// </summary>
    /// <param name="obj"></param>
    private (MoveModel, MoveView) GetMoveSystem(GameObject obj, PathFind pathFind, IMoveNotice statusModel)
    {
        //移動システムの生成
        MoveView playerView = obj.AddComponent<MoveView>();

        //エージェント生成
        IPathAgenter pathAgenter = new PathAgent(pathFind, playerView.transform);

        //プレイヤーの移動機能の生成
        MoveModel playerMoveModel = new MoveModel(pathAgenter, statusModel);
        new MovePresenter(playerMoveModel, playerView).Bind();

        //キャラクターの情報注入
        _updateHandlers.Add(playerMoveModel);

        return (playerMoveModel, playerView); 
    }

    /// <summary>
    /// アクションモデルの取得
    /// </summary>
    /// <returns></returns>
    private (List<ActionModelBase>, List<ActionViewBase>) GetActionModels()
    {
        List<ActionModelBase> actionModelBases = new List<ActionModelBase>();

        List<ActionViewBase> actionViewBases = new List<ActionViewBase>();

        //ファクトリーからアクションのモデルとviewを生成
        ActionMVPData actionMVPData = _actionViewBasePrefab.CreateAction(this.transform, _attackService);
        actionViewBases.Add(actionMVPData.View);
        actionModelBases.Add(actionMVPData.Model);

        return (actionModelBases,actionViewBases);
    }

    /// <summary>
    /// コライダーを付与するメソッドs
    /// </summary>
    /// <param name="character">キャラクター</param>
    /// <param name="characterState">ステータス</param>
    private void AttachCollider(GameObject character, CharacterStatusModel characterState)
    {
        if (!character.TryGetComponent<Collider>(out Collider collider))
        {
            collider = character.AddComponent<CapsuleCollider>();
        }

        _attackService.AddDamageNotice(characterState, collider);
        _targetSelectionModel.AddCharacterStatus(characterState, collider);
    }
}

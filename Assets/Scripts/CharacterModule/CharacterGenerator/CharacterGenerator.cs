using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        PlayerCharacterContModel playerCharacterContModel,
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

        //プレイヤーのインプット情報依存注入
        MoveView playerView = player.AddComponent<MoveView>();
        playerView.SetInput(input);
        TargetSelectionView targetSelectionView = player.AddComponent<TargetSelectionView>();
        targetSelectionView.SetInputSelect(input);

        IPathAgenter path = new PathAgent(pathFind, playerView.transform);

        //プレイヤーの移動機能の生成
        MoveModel playerMoveModel = new MoveModel(path, characterStatusModel);
        new MovePresenter(playerMoveModel, playerView).Bind();

        //プレイヤーのターゲット選択機能の生成
        new TargetSelectionPresenter(_targetSelectionModel, targetSelectionView, statusView).Bind();

        ActionMVPData actionMVPData = _actionViewBasePrefab.CreateAction(this.transform, _attackService);
        //プレイヤーのアクション機能の生成
        List<ActionModelBase> actionBases = new List<ActionModelBase>();
        actionBases.Add(actionMVPData.Model);
        ActionContModelBase actionContModel = new PlayerActionContModel(characterStatusModel, playerMoveModel, actionBases, _targetSelectionModel);

        //プレイヤーのアクションビューの生成
        List<ActionViewBase> actionViewBases = new List<ActionViewBase>();
        actionViewBases.Add(actionMVPData.View);
        ActionContView actionContView = player.AddComponent<ActionContView>();
        actionContView.SetActionView(actionViewBases);

        new ActionContPresenter(actionContModel, actionContView, highlightsView).Bind();

        //キャラクターの情報注入
        _updateHandlers.Add(playerMoveModel);

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
            CpuCharacterContModel cpuCharacterContModel = new CpuCharacterContModel();

            _characterStateHandlers.Add(cpuCharacterContModel);

            SetupCpuCharacter(cpuCharacterContModel);
        }

        return _characterStateHandlers;
    }

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
    /// コライダーを付与するメソッド
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

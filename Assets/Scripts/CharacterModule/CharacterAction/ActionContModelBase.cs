using System;
using System.Collections.Generic;
using UnityEngine;
using R3;

public abstract class ActionContModelBase : IInitialize, ISetActionModel
{
    /// <summary>
    /// 現在選択している行動
    /// </summary>
    protected ReactiveProperty<ActionModelBase>  _rPCurrentAction = new ReactiveProperty<ActionModelBase>();
    public ReadOnlyReactiveProperty<ActionModelBase> RPCurrentAction => _rPCurrentAction;

    /// <summary>
    /// 影響範囲にいるターゲット
    /// </summary>
    protected ReactiveProperty<List<Collider>> _rPTargets = new ReactiveProperty<List<Collider>>();
    public ReadOnlyReactiveProperty<List<Collider>> RPTargets => _rPTargets;

    /// <summary>
    /// キャラクターの位置
    /// </summary>
    protected Vector3 _characterPos = default;
    public Vector3 CharacterPos => _characterPos;

    /// <summary>
    /// キャラクターの派閥
    /// </summary>
    protected IFactionMember _faction = default;
    public IFactionMember Faction => _faction;

    protected IActionNotice _actionNotice;

    protected List<ActionModelBase> _actionList = new List<ActionModelBase>();

    public void Initialize()
    {
        _rPCurrentAction = new ReactiveProperty<ActionModelBase>();
        _rPTargets = new ReactiveProperty<List<Collider>>();
    }

    public void SetActionModel(ActionModelBase characterAction)
    {
        _rPCurrentAction.Value = characterAction;
    }

    public void SetScopeTarget(List<Collider> targets)
    {
        _rPTargets.Value = targets;
    }

    protected void SetCharacterPos(Vector3 pos)
    {
        _characterPos = pos;
    }
}

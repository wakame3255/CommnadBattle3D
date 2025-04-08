using System;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class SelectionTargetData
{
    private Collider _collider;
    private bool _isSelected;

    public Collider Collider => _collider;
    public bool IsSelected => _isSelected;

    public SelectionTargetData(Collider collider, bool isSelected)
    {
        _collider = collider;
        _isSelected = isSelected;
    }
}
public abstract class ActionControllerModelBase : IInitialize, ISetActionModel
{
    /// <summary>
    /// 現在選択している行動
    /// </summary>
    protected ReactiveProperty<ActionModelBase>  _rPCurrentAction = new ReactiveProperty<ActionModelBase>();
    public ReadOnlyReactiveProperty<ActionModelBase> RPCurrentAction => _rPCurrentAction;

    /// <summary>
    /// 影響範囲にいるターゲット
    /// </summary>
    protected ReactiveProperty<List<SelectionTargetData>> _rPTargets = new ReactiveProperty<List<SelectionTargetData>>();
    public ReadOnlyReactiveProperty<List<SelectionTargetData>> RPTargets => _rPTargets;

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
        _rPTargets = new ReactiveProperty<List<SelectionTargetData>>();
    }

    /// <summary>
    /// 現在選択されている行動を格納
    /// </summary>
    /// <param name="characterAction"></param>
    public void SetActionModel(ActionModelBase characterAction)
    {
        _rPCurrentAction.Value = characterAction;
    }

    /// <summary>
    /// 範囲内のターゲットを格納
    /// </summary>
    /// <param name="targets"></param>
    public void SetScopeTarget(List<Collider> targets)
    {
        List<SelectionTargetData> targetList = new List<SelectionTargetData>();

        foreach (Collider target in targets)
        {
            SelectionTargetData targetData = new SelectionTargetData(target, false);
            targetList.Add(targetData);
        }

        _rPTargets.Value = targetList;
    }

    protected void SetCharacterPosition(Vector3 pos)
    {
        _characterPos = pos;
    }
}

using System;

public abstract class ActionModelBase
{
    /// <summary>
    /// 攻撃可能範囲
    /// </summary>
    protected float _attackRange;

    public float AttackRange => _attackRange;

    /// <summary>
    /// 攻撃効果範囲
    /// </summary>
    protected float _scopeOfEffect;

    public float ScopeOfEffect => _scopeOfEffect;

    /// <summary>
    /// 通知先
    /// </summary>
    protected ISetActionModel _actionNotifier;

    /// <summary>
    /// 所属派閥
    /// </summary>
    protected IFactionMember _owner;

    /// <summary>
    /// 攻撃を行うサービス
    /// </summary>
    protected IAttackHandler _attackService;

    /// <summary>
    /// 行動を実行する（引数としてアクション対象を選択？）
    /// </summary>
    public abstract void DoAction();

    /// <summary>
    /// 行動選択を通知する
    /// </summary>
    public void NoticeActionModel()
    {
        _actionNotifier?.SetActionModel(this);
        DoAction();
    }

    /// <summary>
    /// 通知先の注入
    /// </summary>
    /// <param name="actionCont">通知先</param>
    public void SetActionCont(ActionContModel actionCont)
    {
        _actionNotifier = actionCont;
        _owner = actionCont.Faction;
    }
}

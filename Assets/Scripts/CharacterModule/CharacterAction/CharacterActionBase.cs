using System;

public abstract class CharacterActionBase
{
    /// <summary>
    /// 攻撃可能範囲
    /// </summary>
    protected float _attackRange;

    /// <summary>
    /// 攻撃効果範囲
    /// </summary>
    protected float _scopeOfEffect;

    /// <summary>
    /// 行動を実行する（引数としてアクション対象を選択？）
    /// </summary>
    public abstract void DoAction();
}
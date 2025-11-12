using System;

/// <summary>
/// 陣営状態の基底クラス
/// 各陣営の状態遷移を管理するためのテンプレート
/// </summary>
public abstract class FactionStateBase
{
    /// <summary>
    /// 状態に入る際の処理
    /// </summary>
    public abstract void EnterState();

    /// <summary>
    /// 状態中の更新処理
    /// </summary>
    public abstract void UpdateState();

    /// <summary>
    /// 状態から出る際の処理
    /// </summary>
    public abstract void ExitState();
}

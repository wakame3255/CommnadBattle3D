using System;
using R3;

/// <summary>
/// アクションコストの通知を提供するインターフェース
/// アクション実行時のコスト情報をリアクティブに通知
/// </summary>
public interface IActionNotice
{
    /// <summary>
    /// アクションコストを通知するリアクティブプロパティ
    /// </summary>
    public ReadOnlyReactiveProperty<int> RPActionCost { get; }

    /// <summary>
    /// アクションコストの使用を通知
    /// </summary>
    /// <param name="actionCost">使用したアクションコスト</param>
    public void NotifyUseActionCost(int actionCost);
}

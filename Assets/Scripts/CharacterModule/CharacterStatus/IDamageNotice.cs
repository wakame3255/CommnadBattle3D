using System;

/// <summary>
/// ダメージ通知を受け取るインターフェース
/// キャラクターがダメージを受けた際の処理を定義
/// </summary>
public interface IDamageNotice
{

    /// <summary>
    /// ダメージを通知
    /// </summary>
    /// <param name="damage">受けたダメージ量</param>
    public void NotifyDamage(int damage);
}
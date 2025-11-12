using System;

/// <summary>
/// キャラクターの所属陣営を表す列挙型
/// </summary>
public enum Faction
{
    /// <summary>プレイヤー陣営</summary>
    Player,
    /// <summary>敵陣営</summary>
    Enemy,
    /// <summary>中立陣営</summary>
    Neutral
}

/// <summary>
/// 陣営所属を示すインターフェース
/// キャラクターの敵味方判定に使用
/// </summary>
public interface IFactionMember
{
    /// <summary>
    /// キャラクターの所属陣営
    /// </summary>
    Faction Faction { get; }
}

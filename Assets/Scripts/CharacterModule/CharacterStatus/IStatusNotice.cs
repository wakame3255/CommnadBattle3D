using R3;
using System;
using UnityEngine;

public interface IStatusNotice
{
    /// <summary>
    /// 公開用HP
    /// </summary>
    public ReadOnlyReactiveProperty<int> RPHealth { get; }

    /// <summary>
    /// 公開用派閥
    /// </summary>
    public Faction Faction { get; }

    public Vector3 NowPosition { get; }
}

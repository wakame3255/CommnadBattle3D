using R3;
using System;
using UnityEngine;

public interface IMoveNotice
{
    /// <summary>
    /// 残りの移動距離
    /// </summary>
    public ReadOnlyReactiveProperty<float> TravelDistance { get; }

    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get; }

    /// <summary>
    /// 移動距離の通知
    /// </summary>
    /// <param name="moveDistance"></param>
    public void NotifyMove(float moveDistance, Vector3 nowPos);
}

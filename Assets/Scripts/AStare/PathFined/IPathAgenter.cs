using System;
using UnityEngine;

/// <summary>
/// パス検索エージェントのインターフェース
/// 経路を設定し、次の移動先を取得する機能を提供
/// </summary>
public interface IPathAgenter
{
    /// <summary>
    /// カスタムパスを設定
    /// </summary>
    /// <param name="pointerPosition">目標地点の位置</param>
    public void SetCustomPath(Vector3 pointerPosition);

    /// <summary>
    /// 次の移動先パスを取得
    /// </summary>
    /// <param name="nowPos">現在位置</param>
    /// <returns>次の移動先の座標</returns>
    public System.Numerics.Vector3 GetNextPath(Vector3 nowPos);
}
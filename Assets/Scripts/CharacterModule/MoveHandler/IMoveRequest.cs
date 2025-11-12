using System;

/// <summary>
/// 移動要求を受け付けるインターフェース
/// キャラクターの移動制御に使用
/// </summary>
public interface IMoveRequest
{
    /// <summary>
    /// 指定位置への移動を要求
    /// </summary>
    /// <param name="pos">移動先の座標</param>
    public void MovePosition(System.Numerics.Vector3 pos);

    /// <summary>
    /// 移動を停止
    /// </summary>
    public void MoveStop();
}

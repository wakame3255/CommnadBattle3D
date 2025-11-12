using R3;
using UnityEngine;

/// <summary>
/// 入力情報を提供するインターフェース
/// マウスやタッチなどのポインター位置とコライダー情報を取得
/// </summary>
public interface IInputInformation
{
    /// <summary>
    /// ポインターの3D空間上の位置
    /// </summary>
    public ReadOnlyReactiveProperty<Vector3> PointerPosition { get; }

    /// <summary>
    /// ポインターが指しているオブジェクトのコライダー
    /// </summary>
    public ReadOnlyReactiveProperty<Collider> PointerCollider { get; }
}

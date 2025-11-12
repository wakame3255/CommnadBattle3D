using R3;
using System;

/// <summary>
/// 位置情報の通知を提供するインターフェース
/// キャラクターの現在位置をリアクティブプロパティで提供
/// </summary>
public interface INoticePosition
{
    /// <summary>
    /// Transform の位置を通知するリアクティブプロパティ
    /// </summary>
    public ReadOnlyReactiveProperty<System.Numerics.Vector3> RPTransformPosition { get; }
}

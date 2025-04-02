using R3;
using System;

public interface INoticePosition
{
    public ReadOnlyReactiveProperty<System.Numerics.Vector3> RPTransformPosition { get; }
}

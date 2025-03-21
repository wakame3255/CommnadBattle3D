using R3;
using UnityEngine;

public interface IInputInfomation
{
    public ReadOnlyReactiveProperty<Vector3> PointerPosition { get; }
}
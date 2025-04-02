using R3;
using UnityEngine;

public interface IInputInformation
{
    public ReadOnlyReactiveProperty<Vector3> PointerPosition { get; }

    public ReadOnlyReactiveProperty<Collider> PointerCollider { get; }
}

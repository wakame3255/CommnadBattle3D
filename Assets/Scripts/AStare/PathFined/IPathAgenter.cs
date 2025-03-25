using System;
using UnityEngine;

public interface IPathAgenter
{
    public void SetCustomPath(Vector3 pointerPosition);

    public System.Numerics.Vector3 GetNextPath(Vector3 nowPos);
}
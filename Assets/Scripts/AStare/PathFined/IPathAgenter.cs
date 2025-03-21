using System;
using UnityEngine;

public interface IPathAgenter
{
    public void SetCustomPath(Vector3 pointerPosition);

    public Vector3 GetNextPath(Vector3 nowPos);
}
using System;
using UnityEngine;

public class MoveView : MonoBehaviour, IInitialize
{
    public void Initialize()
    {

    }
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
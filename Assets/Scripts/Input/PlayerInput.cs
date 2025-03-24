using System;
using UnityEngine;
using R3;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IInputInformation
{

    private ReactiveProperty<Vector3> _pointerPositionRP = new ReactiveProperty<Vector3>();

    public ReadOnlyReactiveProperty<Vector3> PointerPosition => _pointerPositionRP;

    private void Update()
    {
        GetMousePosition();       
    }

    private void GetMousePosition()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                _pointerPositionRP.Value = hit.point;
            }
        }     
    }
}

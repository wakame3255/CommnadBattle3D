using System;
using UnityEngine;
using R3;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IInputInformation
{
    /// <summary>
    /// マウスの位置
    /// </summary>
    private ReactiveProperty<Vector3> _pointerPositionRP = new ReactiveProperty<Vector3>();

    public ReadOnlyReactiveProperty<Vector3> PointerPosition => _pointerPositionRP;

    /// <summary>
    /// マウスの位置にあるコライダー
    /// </summary>
    private ReactiveProperty<Collider> _pointerColliderRP = new ReactiveProperty<Collider>();
    public ReadOnlyReactiveProperty<Collider> PointerCollider => _pointerColliderRP;
    private void Update()
    {
        GetMousePosition();       
    }

    /// <summary>
    /// マウスの位置を取得するメソッド
    /// </summary>
    private void GetMousePosition()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                _pointerPositionRP.Value = hit.point;
                _pointerColliderRP.Value = hit.collider;
            }
        }     
    }
}

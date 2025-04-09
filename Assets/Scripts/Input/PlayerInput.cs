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
        // マウスの位置を取得
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            _pointerPositionRP.Value = ReturnHitInfo().point;
        }

        // マウスの位置にあるコライダーを取得
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _pointerColliderRP.Value = null;
            _pointerColliderRP.Value = ReturnHitInfo().collider;
            //DebugUtility.Log(_pointerColliderRP.Value.ToString());
        }
    }

    /// <summary>
    /// マウスの位置にあるhit情報を取得するメソッド
    /// </summary>
    /// <returns></returns>
    private RaycastHit ReturnHitInfo()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return hit;
        }

        return default;
    }
}

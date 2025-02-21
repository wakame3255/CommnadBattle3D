using R3;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private ReactiveProperty<Vector2> _reactivePropertyMove = new ReactiveProperty<Vector2>();
    private ReactiveProperty<bool> _reactivePropertyAttack = new ReactiveProperty<bool>();
    private ReactiveProperty<bool> _reactivePropertyAvoidance = new ReactiveProperty<bool>();
    private ReactiveProperty<bool> _reactivePropertyDash = new ReactiveProperty<bool>();
    private ReactiveProperty<bool> _reactivePropertyGuard = new ReactiveProperty<bool>();

    public ReactiveProperty<bool> ReactivePropertyAttack { get => _reactivePropertyAttack; }
    public ReactiveProperty<bool> ReactivePropertyAvoidance { get => _reactivePropertyAvoidance; }
    public ReactiveProperty<bool> ReactivePropertyDash { get => _reactivePropertyDash; }
    public ReactiveProperty<bool> ReactivePropertyGuard { get => _reactivePropertyGuard; }
    public ReactiveProperty<Vector2> ReactivePropertyMove { get => _reactivePropertyMove; }

    private void Update()
    {
        CheckKeyBoardDevice();
        SetButton();
    }

    private void CheckKeyBoardDevice()
    {
        if (Keyboard.current != null)
        {
            SetMoveInfomation();
        }
    }

    private void SetMoveInfomation()
    {
        float inputX = Keyboard.current.aKey.isPressed ? -1 : Keyboard.current.dKey.isPressed ? 1 : 0;
        float inputY = Keyboard.current.sKey.isPressed ? -1 : Keyboard.current.wKey.isPressed ? 1 : 0;

        _reactivePropertyMove.Value = new Vector2(inputX, inputY);
    }

    private void SetButton()
    {
        _reactivePropertyAttack.Value = Mouse.current.leftButton.wasPressedThisFrame;

        _reactivePropertyGuard.Value = Mouse.current.rightButton.wasPressedThisFrame;

        _reactivePropertyAvoidance.Value = Keyboard.current.spaceKey.wasPressedThisFrame;

        _reactivePropertyDash.Value = Keyboard.current.leftShiftKey.isPressed;

        
    }

}

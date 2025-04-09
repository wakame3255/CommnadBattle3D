using System;
using UnityEngine;
using UnityEngine.UI;
using R3;
using System.Collections.Generic;
using VContainer;

public class TargetSelectionView : MonoBehaviour
{
    private ReactiveProperty<Collider> _rPSelectedTarget = new ReactiveProperty<Collider>();
    public ReadOnlyReactiveProperty<Collider> RPSelectedTarget => _rPSelectedTarget;

    /// <summary>
    /// インプットの受け取り
    /// </summary>
    /// <param name="input"></param>
    public void SetInputSelect(IInputInformation input)
    {
        input.PointerCollider.Subscribe(SetTargetCollider).AddTo(this);
    }

    /// <summary>
    /// ターゲットのコライダーを設定
    /// </summary>
    /// <param name="collider"></param>
    private void SetTargetCollider(Collider collider)
    {
        _rPSelectedTarget.Value = collider;
        
    }
}

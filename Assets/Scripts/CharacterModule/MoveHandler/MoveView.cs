using System;
using UnityEngine;
using R3;

public class MoveView : MonoBehaviour, IInitialize
{
    private ReactiveProperty<Vector3> _rPClickPos = new ReactiveProperty<Vector3>();

    public ReadOnlyReactiveProperty<Vector3> RPClickPos => _rPClickPos;

    /// <summary>
    /// 入力情報の購読
    /// </summary>
    /// <param name="inputInfomation">インプット情報</param>
    public void SetInput(IInputInformation inputInfomation)
    {
        
        inputInfomation.PointerPosition.Subscribe(OnClick).AddTo(this);
    }

    public void Initialize()
    {
       
    }

    /// <summary>
    /// 位置の設定
    /// </summary>
    /// <param name="position">現在地</param>
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    /// <summary>
    /// クリック位置の更新
    /// </summary>
    /// <param name="clickPos">クリック場所</param>
    private void OnClick(Vector3 clickPos)
    {
       _rPClickPos.Value = clickPos;

        //DebugUtility.Log("ClickPos" + clickPos);
    }
}

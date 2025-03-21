using System;
using System.Numerics;
using R3;

public class MoveModel : IInitialize
{
    private IPathAgenter _pathAgent;

    private IMoveNotice _moveNotice;

    private ReactiveProperty<Vector3> _rPTransformPosition;

    public ReadOnlyReactiveProperty<Vector3> RPTransformPosition { get => _rPTransformPosition; }

    public MoveModel(IPathAgenter pathAgent, IMoveNotice moveNotice)
    {
        _pathAgent = pathAgent;
        _moveNotice = moveNotice;
    }

    public void Initialize()
    {
       
    }

    /// <summary>
    /// 初期位置の設定
    /// </summary>
    /// <param name="position">ポジション</param>
    public void SetPosition(Vector3 position)
    {
        _rPTransformPosition.Value = position;
    }
}
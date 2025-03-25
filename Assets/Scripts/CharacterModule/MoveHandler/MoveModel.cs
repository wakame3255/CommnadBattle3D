using System;
using System.Numerics;
using R3;

public class MoveModel : IInitialize, IUpdateHandller
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
        _rPTransformPosition = new ReactiveProperty<Vector3>();
    }

    public void Updateable()
    {

    }

    /// <summary>
    /// 目的地の設定
    /// </summary>
    /// <param name="pos">目的地</param>
    public void MovePotision(Vector3 pos)
    {
        _pathAgent.SetCustomPath(Vector3Extensions.ToUnityVector3(pos));
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
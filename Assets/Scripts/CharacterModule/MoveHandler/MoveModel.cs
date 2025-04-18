using System;
using System.Numerics;
using R3;

public class MoveModel : IInitialize, IUpdateHandler, INoticePosition, IMoveRequest
{
    /// <summary>
    /// 移動先のパスを取得するインターフェース
    /// </summary>
    private IPathAgenter _pathAgent;

    /// <summary>
    /// 移動通知を行うインターフェース
    /// </summary>
    private IMoveNotice _moveNotice;

    private bool _isMoveEnd = false;

    /// <summary>
    /// 位置情報のイベント発行
    /// </summary>
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

        _moveNotice.TravelDistance.Subscribe(UpdateTravelDistance);
    }

    public void Updateable()
    {
        // 移動先のパスを取得
        Vector3 moveDirection = _pathAgent.GetNextPath(Vector3Extensions.ToUnityVector3(_rPTransformPosition.Value));

        if (moveDirection == Vector3.Zero || _isMoveEnd)
        {
            return;
        }

        //パスに向かって移動
        _rPTransformPosition.Value += moveDirection * 0.02f;
        //移動距離の通知
        _moveNotice.NotifyMove(0.02f);
    }

    /// <summary>
    /// 目的地の設定
    /// </summary>
    /// <param name="pos">目的地</param>
    public void MovePosition(Vector3 pos)
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

    public void MoveStop()
    {
        // 移動を停止
        SetPosition(_rPTransformPosition.Value);
    }

    /// <summary>
    /// 残りの移動距離更新
    /// </summary>
    /// <param name="travelDistance">残り移動距離</param>
    private void UpdateTravelDistance(float travelDistance)
    {
        if (travelDistance <= 0)
        {
            _isMoveEnd = true;

            _pathAgent.SetCustomPath(Vector3Extensions.ToUnityVector3(_rPTransformPosition.CurrentValue));
        }
        else
        {
            _isMoveEnd = false;
        }
    }
}

using R3;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent :  IPathAgenter
{
    private PathFind _pathFind = default;

    private Transform _transform = default;

    private List<Vector3> wayPoints = new List<Vector3>();

    private int _currentWayPointIndex = 0;

    public PathAgent(PathFind pathFind, Transform transform)
    {
        _pathFind = pathFind;
        _transform = transform;
    }

    /// <summary>
    /// 目的地の設定
    /// </summary>
    /// <param name="pointerPosition">目的地</param>
    public void SetCustomPath(Vector3 pointerPosition)
    {
        Node startNode = _pathFind?.GetNodeWorldPosition(_transform.position);
        Node endNode = _pathFind?.GetNodeWorldPosition(pointerPosition);

        //経路探索
        List<Node> path = _pathFind?.ReturnFindTacticalPath(startNode, endNode);

        if (path != null)
        {
            wayPoints = path.ConvertAll(node => node.Position);
            _currentWayPointIndex = 0;
        }  
    }

    /// <summary>
    /// 次の目的地の向きを取得するメソッド(Updateで呼んでください)
    /// </summary>
    /// <param name="nowPos">現在の位置</param>
    /// <returns>次の目的地への向き</returns>
    public System.Numerics.Vector3 GetNextPath(Vector3 nowPos)
    {
        //経路がない場合は0を返す,目的地居ついたら0を返す
        if (wayPoints.Count == 0 || _currentWayPointIndex >= wayPoints.Count)
        {
            return Vector3Extensions.ToSystemVector3(Vector3.zero);
        }

        //次の目的地を取得
        Vector3 targetPosition = wayPoints[_currentWayPointIndex];
        Vector3 moveDirection = targetPosition - _transform.position;

        //目的地に到達したら次の目的地へ
        if (Vector3.Distance(nowPos, targetPosition) < 0.1f)
        {
            _currentWayPointIndex++;
        }

        return Vector3Extensions.ToSystemVector3(moveDirection.normalized);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class PathAgent : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private GridGeneratePresenter _gridGeneratePresenter = default;

    [SerializeField]
    private Transform _goalPosition = default;

    private PathFind _pathFind = default;

    private Transform _transform = default;

    private List<Vector3> wayPoints = new List<Vector3>();

    private int _currentWayPointIndex = 0;

    private void Awake()
    {
        _transform = this.transform;
        _pathFind = new PathFind(_gridGeneratePresenter);
    }

    private void Start()
    {
        SetCustomPath();
    }

    private void Update()
    {
        FollowCustomPath();
    }

    private void SetCustomPath()
    {
        Node startNode = _gridGeneratePresenter.GetNodeWorldPosition(_transform.position);
        Node endNode = _gridGeneratePresenter.GetNodeWorldPosition(_goalPosition.position);

        List<Node> path = _pathFind.ReturnFindTacticalPath(startNode, endNode);
        wayPoints = path.ConvertAll(node => node.Position);
    }

    private void FollowCustomPath()
    {
        if (wayPoints.Count == 0)
        {
            return;
        }
        if (_currentWayPointIndex >= wayPoints.Count)
        {
            return;
        }
        Vector3 targetPosition = wayPoints[_currentWayPointIndex];
        Vector3 moveDirection = targetPosition - _transform.position;
        _transform.position += moveDirection.normalized * Time.deltaTime * _speed;
        if (Vector3.Distance(_transform.position, targetPosition) < 0.1f)
        {
            _currentWayPointIndex++;
        }
    }
}
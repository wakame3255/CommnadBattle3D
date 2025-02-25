using UnityEngine;

public class Node
{
    private Vector3 _position;
    private bool _isWalkable;

    public Vector3 Position { get => _position; }
    public bool IsWalkable { get => _isWalkable; }

    public Node(Vector3 position, bool isWalkable = false)
    {
        _position = position;
        _isWalkable = isWalkable;
    }
}
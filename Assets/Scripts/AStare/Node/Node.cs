using UnityEngine;

/// <summary>
/// A*パスファインディングで使用するノードクラス
/// グリッド上の1マスを表現し、位置と通行可能性を保持
/// </summary>
public class Node
{
    private Vector3 _position;
    private bool _isWalkable;

    /// <summary>
    /// ノードの3D空間上の位置
    /// </summary>
    public Vector3 Position { get => _position; }
    
    /// <summary>
    /// このノードが通行可能かどうか
    /// </summary>
    public bool IsWalkable { get => _isWalkable; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="position">ノードの位置</param>
    /// <param name="isWalkable">通行可能かどうか（デフォルト: false）</param>
    public Node(Vector3 position, bool isWalkable = false)
    {
        _position = position;
        _isWalkable = isWalkable;
    }
}
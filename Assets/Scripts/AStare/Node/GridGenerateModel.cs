using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerateData
{
    private int _gridSizeX = default;
    private int _gridSizeZ = default;
    private float _gridCellSize = default;

    public int GridSizeX { get => _gridSizeX; }
    public int GridSizeZ { get => _gridSizeZ; }
    public float GridCellSize { get => _gridCellSize; }

    public GridGenerateData(int gridSizeX, int gridSizeZ, float gridCellSize)
    {
        _gridCellSize = gridCellSize;
        _gridSizeX = gridSizeX;
        _gridSizeZ = gridSizeZ;
    }
}
/// <summary>
/// グリッドの生成（画面レベル）
/// </summary>
public class GridGenerateModel
{

    private int _gridSizeX = 10;

    private int _gridSizeZ = 10;

    private float _gridCellSize = 1.0f;

    private Transform _gridPos = default;

    private ReactiveProperty<Node[,]> _grid;

    /// <summary>
    /// 密度レベル表示データ
    /// </summary>
    readonly Dictionary<Vector3, int> nodeSectorData = new();

    public ReadOnlyReactiveProperty<Node[,]> Grid { get => _grid; }

    //初期化データクラス用
    public GridGenerateModel(GridGenerateData generateData, Transform gridPos)
    {
        _gridSizeX = generateData.GridSizeX;
        _gridSizeZ = generateData.GridSizeZ;
        _gridCellSize = generateData.GridCellSize;
        _gridPos = gridPos;

        _grid = new ReactiveProperty<Node[,]>();
    }

    /// <summary>
    /// グリッドの生成
    /// </summary>
    public void InitializeGrid()
    {
        Node[,] node = new Node[_gridSizeX, _gridSizeZ];

        //グリッドのマスを繰り返す
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int z = 0; z < _gridSizeZ; z++)
            {
                Vector3 pos = default;

                //ノードの位置をマップ位置
                pos.Set(x * _gridCellSize, _gridPos.position.y, z * _gridCellSize);

                //実際のノード位置
                pos.y = GetNodeYPosition(pos);

                //グリッドのマスの生成(歩行できるかどうかの取得を行い、ブールに値を格納)
                node[x, z] = new Node(pos, true);
                //ノードの位置をマップ位置
                pos = new Vector3(x * _gridCellSize, _gridPos.position.y, z * _gridCellSize);

                //実際のノード位置
                pos.y = GetNodeYPosition(pos);

                //グリッドのマスの生成(歩行できるかどうかの取得を行い、ブールに値を格納)
                node[x, z] = new Node(pos, true);
            }
        }
        _grid.Value = node;
    }

    public Node GetNodeFromWorldPosition(Vector3 worldPosition)
    {
        int xPos = Mathf.RoundToInt(worldPosition.x / _gridCellSize);
        int zPos = Mathf.RoundToInt(worldPosition.z / _gridCellSize);
        xPos = Mathf.Clamp(xPos, 0, _gridSizeX - 1);
        zPos = Mathf.Clamp(zPos, 0, _gridSizeZ - 1);
        return _grid.Value[xPos, zPos];
    }

    private float GetNodeYPosition(Vector3 rayShotPos)
    {
        if (Physics.BoxCast(rayShotPos, Vector3.one * (_gridCellSize / 2), Vector3.down, out RaycastHit hitInfo, Quaternion.identity, Mathf.Infinity))
        {
            return hitInfo.point.y;
        }

        return rayShotPos.y;
    }
}

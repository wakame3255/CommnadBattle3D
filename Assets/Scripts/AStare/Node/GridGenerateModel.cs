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
/// グリッドの生成（上位レベル）
/// </summary>
public class GridGenerateModel
{
   
    private int _gridSizeX = 10;
   
    private int _gridSizeZ = 10;
   
    private float _gridCellSize = 1.0f;

    private ReactiveProperty<Node[,]> _grid;

    /// <summary>
    /// 危険度を表すデータ
    /// </summary>
    readonly Dictionary<Vector3, int> nodeSectorData = new();

    public ReadOnlyReactiveProperty<Node[,]> Grid { get => _grid; }

    //引数をデータクラス化
    public GridGenerateModel(GridGenerateData generateData)
    {
        _gridSizeX = generateData.GridSizeX;
        _gridSizeZ = generateData.GridSizeZ;
        _gridCellSize = generateData.GridCellSize;

        _grid = new ReactiveProperty<Node[,]>();
    }

    /// <summary>
    /// グリッドの生成
    /// </summary>
    public void InitializeGrid()
    {
        Node[,] node = new Node[_gridSizeX, _gridSizeZ];

        //グリッドのマス分繰り返す
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int z = 0; z < _gridSizeZ; z++)
            {
                Vector3 pos = default;
                pos.Set(x * _gridCellSize, 0, z * _gridCellSize);

                //グリッドのマスの生成(今後壁があるかの取得を行い、ブールに値を格納)
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
}
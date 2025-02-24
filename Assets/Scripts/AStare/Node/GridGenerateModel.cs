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
/// �O���b�h�̐����i��ʃ��x���j
/// </summary>
public class GridGenerateModel
{
   
    private int _gridSizeX = 10;
   
    private int _gridSizeZ = 10;
   
    private float _gridCellSize = 1.0f;

    private ReactiveProperty<Node[,]> _grid;

    /// <summary>
    /// �댯�x��\���f�[�^
    /// </summary>
    readonly Dictionary<Vector3, int> nodeSectorData = new();

    public ReadOnlyReactiveProperty<Node[,]> Grid { get => _grid; }

    //�������f�[�^�N���X��
    public GridGenerateModel(GridGenerateData generateData)
    {
        _gridSizeX = generateData.GridSizeX;
        _gridSizeZ = generateData.GridSizeZ;
        _gridCellSize = generateData.GridCellSize;

        _grid = new ReactiveProperty<Node[,]>();
    }

    /// <summary>
    /// �O���b�h�̐���
    /// </summary>
    public void InitializeGrid()
    {
        Node[,] node = new Node[_gridSizeX, _gridSizeZ];

        //�O���b�h�̃}�X���J��Ԃ�
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int z = 0; z < _gridSizeZ; z++)
            {
                Vector3 pos = default;
                pos.Set(x * _gridCellSize, 0, z * _gridCellSize);

                //�O���b�h�̃}�X�̐���(����ǂ����邩�̎擾���s���A�u�[���ɒl���i�[)
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
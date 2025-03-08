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

    private Transform _gridPos = default;

    private ReactiveProperty<Node[,]> _grid;

    /// <summary>
    /// �댯�x��\���f�[�^
    /// </summary>
    readonly Dictionary<Vector3, int> nodeSectorData = new();

    public ReadOnlyReactiveProperty<Node[,]> Grid { get => _grid; }

    //�������f�[�^�N���X��
    public GridGenerateModel(GridGenerateData generateData, Transform gridPos)
    {
        _gridSizeX = generateData.GridSizeX;
        _gridSizeZ = generateData.GridSizeZ;
        _gridCellSize = generateData.GridCellSize;
        _gridPos = gridPos;

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

                //�m�[�h�̈ʒu��}��ʒu
                pos.Set(x * _gridCellSize, _gridPos.position.y, z * _gridCellSize);

                //���ۂ̃m�[�h�ʒu
                pos.y = GetNodeYPosition(pos);

                //�O���b�h�̃}�X�̐���(����ǂ����邩�̎擾���s���A�u�[���ɒl���i�[)
                node[x, z] = new Node(pos, true);
                //�m�[�h�̈ʒu��}��ʒu
                pos = new Vector3(x * _gridCellSize, _gridPos.position.y, z * _gridCellSize);

                //���ۂ̃m�[�h�ʒu
                pos.y = GetNodeYPosition(pos);

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

    private float GetNodeYPosition(Vector3 rayShotPos)
    {
        if (Physics.BoxCast(rayShotPos, Vector3.one * (_gridCellSize / 2), Vector3.down,out RaycastHit hitInfo, Quaternion.identity, Mathf.Infinity))
        {
            return hitInfo.point.y;
        }

        return rayShotPos.y;
    }
}
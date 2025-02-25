
using System.Collections.Generic;
using UnityEngine;

public class GridGenerateView
{
    private GameObject _instanceObj;

    private Material _material = default;
    private Transform _parent = default;

    private GameObject[,] _nodes = default;
    private Node[,] _cacheNodeData = default;

    public GridGenerateView(Material material, Transform parent, GameObject instanceObj)
    {
        _material = material;
        _parent = parent;
        _instanceObj = instanceObj;
    }
    public void UpdateGrid(Node[,] nodeDate)
    {
        _cacheNodeData = nodeDate;

        if (nodeDate == null)
        {
            return;
        }

        int xMax = nodeDate.GetLength(0);
        int zMax = nodeDate.GetLength(1);

        GameObject[,] nodes = new GameObject[xMax, zMax];

        for (int x = 0; x < xMax; x++)
        {
            for (int z = 0; z < zMax; z++)
            {
                GameObject cellObj = MonoBehaviour.Instantiate(_instanceObj);
                cellObj.transform.parent = _parent;
                cellObj.transform.position = nodeDate[x, z].Position;
                //cellObj.transform.rotation = Quaternion.Euler(90, 0, 0);

                nodes[x, z] = cellObj;
            }
        }

        _nodes = nodes;
    }

    public void ChangeColorNode(List<Node> wayPoints)
    {
        int xMax = _cacheNodeData.GetLength(0);
        int zMax = _cacheNodeData.GetLength(1);

        for (int x = 0; x < xMax; x++)
        {
            for (int z = 0; z < zMax; z++)
            {
                if (CheckMatchNode(x, z, wayPoints))
                {
                    ChangeColor(x, z);
                    continue;
                }
            }
        }
    }

    private bool CheckMatchNode(int xIndex, int yIndex, List<Node> nodes)
    {
        foreach (Node node in nodes)
        {
            if (node.Position == _cacheNodeData[xIndex, yIndex].Position)
            {
                return true;
            }    
        }
        return false;
    }

    private void ChangeColor(int x, int z)
    {
        _nodes[x, z].GetComponent<Renderer>().material = _material;
    }
}
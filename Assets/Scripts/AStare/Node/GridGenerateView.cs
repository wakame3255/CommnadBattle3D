
using System.Collections.Generic;
using UnityEngine;

public class GridGenerateView : MonoBehaviour
{
    [SerializeField]
    private GameObject _instanceObj;

    [SerializeField]
    private Material _wayPointMaterial = default;
    [SerializeField]
    private Material _normalMaterial = default;

    private GameObject[,] _nodes = default;
    private Node[,] _cacheNodeData = default;

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
                GameObject cellObj = MonoBehaviour.Instantiate(_instanceObj, this.transform);
                cellObj.transform.position = nodeDate[x, z].Position;

                nodes[x, z] = cellObj;
            }
        }

        _nodes = nodes;
    }

    /// <summary>
    ///    ノードの色を変更する
    /// </summary>
    /// <param name="wayPoints"></param>
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
                    ChangeColor(x, z, true);
                    continue;
                }

                ChangeColor(x, z, false);
            }
        }
    }

    /// <summary>
    ///    ノードが一致しているか確認
    /// </summary>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="nodes"></param>
    /// <returns></returns>
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

    private void ChangeColor(int x, int z, bool isWayPoint)
    {
        if (_nodes[x, z] == null)
        {
            return;
        }

        if (isWayPoint)
        {
            _nodes[x, z].GetComponent<Renderer>().material = _wayPointMaterial;
        }
        else
        {
            _nodes[x, z].GetComponent<Renderer>().material = _normalMaterial;
        }

    }
}
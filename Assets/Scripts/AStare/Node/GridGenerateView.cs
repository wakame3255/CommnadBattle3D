using System;
using UnityEngine;

public class GridGenerateView
{
    private Material _material = default;
    private Transform _parent = default;

    public GridGenerateView(Material material, Transform parent)
    {
        _material = material;
        _parent = parent;
    }
    public void UpdateGrid(Node[,] nodeDate)
    {
        if (nodeDate == null)
        {
            return;
        }

        for (int x = 0; x < nodeDate.GetLength(0); x++)
        {
            for (int z = 0; z < nodeDate.GetLength(1); z++)
            {
                GameObject cellObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
                cellObj.transform.parent = _parent;
                cellObj.transform.position = nodeDate[x, z].Position;
                cellObj.transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }
    }
}
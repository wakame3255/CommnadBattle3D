using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 経路探査（ムーブ系の配下にいるべき？）
/// </summary>
public class PathFind
{
    private GridGeneratePresenter _gridGeneratePresenter;

    public List<Node> ReturnFindTacticalPath(Node startNod, Node endNode)
    {
        //選択した道の優先度を表す
        PriorityQueue<Node, float> openList = new PriorityQueue<Node, float>();

        //いけない道を再評価しないためのリスト
        HashSet<Vector3> closedSet = new HashSet<Vector3>();

        //どこからどこに来たかを記録するリスト
        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();

        //スタートからのコストを記録するリスト
        Dictionary<Vector3, float> costSoFar = new Dictionary<Vector3, float> { [startNod.Position] = 0};

        //スタートからの露出度を記録するリスト
        Dictionary<Vector3, float> exposureSoFar = new Dictionary<Vector3, float> { [startNod.Position] = 0};


        //スタート地点を追加
        openList.Enqueue(startNod, 0);

       while (openList.Count > 0)
        {
            //一番コストが低いノードを取得
            Node currentNode = openList.Dequeue();

            //ゴールなのかの確認
            if (currentNode.Position == endNode.Position)
            {
                //return ReconstructPath(cameFrom, currentNode);
            }

            //評価済みに追加
            closedSet.Add(currentNode.Position);

            foreach(Node neighbor in GetNeighbors(currentNode))
            {
                if (closedSet.Contains(neighbor.Position))
                {
                    continue;
                }
                float newCost = costSoFar[currentNode.Position] + 1;
              
                

            }
        }
        return null;
    }

    private float GetCostNodeToNode(Node fromNode, Node toNode)
    {
        float nodeDistance = Vector3.Distance(fromNode.Position, toNode.Position);
        return nodeDistance;
    }

    IEnumerable<Node> GetNeighbors(Node node)
    {
        Vector3[] direction = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        return direction
            .Select(direction => _gridGeneratePresenter.GetNodeWorldPosition(node.Position + direction * _gridGeneratePresenter.GridCellSize))
            .Where(neighbor => neighbor.IsWalkable);
    }
}
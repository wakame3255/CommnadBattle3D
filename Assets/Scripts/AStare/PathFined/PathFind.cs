using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 経路探査（ムーブ系の配下にいるべき？）
/// </summary>
public class PathFind
{
    private GridGeneratePresenter _gridGeneratePresenter;

    public PathFind(GridGeneratePresenter gridGeneratePresenter)
    {
        _gridGeneratePresenter = gridGeneratePresenter;
    }


    public List<Node> ReturnFindTacticalPath(Node startNode, Node endNode)
    {
        //選択した道の優先度を表す
        PriorityQueue<Node, float> openList = new PriorityQueue<Node, float>();

        //いけない道を再評価しないためのリスト
        HashSet<Vector3> closedSet = new HashSet<Vector3>();

        //どこからどこに来たかを記録するリスト
        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();

        //スタートからのコストを記録するリスト
        Dictionary<Vector3, float> costSoFar = new Dictionary<Vector3, float> { [startNode.Position] = 0};

        //スタートからの露出度を記録するリスト
        Dictionary<Vector3, float> exposureSoFar = new Dictionary<Vector3, float> { [startNode.Position] = 0};


        //スタート地点を追加
        openList.Enqueue(startNode, 0);

       while (openList.Count > 0)
        {
            //一番コストが低いノードを取得
            Node currentNode = openList.Dequeue();

            //ゴールなのかの確認
            if (currentNode.Position == endNode.Position)
            {
                return ReconstructPath(cameFrom, startNode, endNode);
            }

            //評価済みに追加
            closedSet.Add(currentNode.Position);


            //隣接ノードを取得
            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                //評価済みのノードはスキップ
                if (closedSet.Contains(neighbor.Position))
                {
                    continue;
                }
                DebugUtility.Log(costSoFar[currentNode.Position].ToString());
                //コストの計算
                float newCost = costSoFar[currentNode.Position] + GetCostNodeToNode(currentNode, neighbor);

                //最小コストで開ければ再探査
                if (costSoFar.TryGetValue(neighbor.Position, out float existingCost) && newCost >= existingCost)
                {
                    continue;
                }

                //最小コストの更新
                costSoFar[neighbor.Position] = newCost;

                //どのノードからどのノードに来たかを記録
                cameFrom[neighbor.Position] = currentNode.Position;

                //ノード間の移動コストとゴールまでの距離を加味して優先度を計算
                float priority = newCost + GetCostNodeToNode(neighbor, endNode);

                //ゴールに近いのノードとして追加
                openList.Enqueue(neighbor, priority);
            }
        }
       Debug.Log("経路が見つかりませんでした");

        return null;
    }

    /// <summary>
    /// ノード間のコストを取得
    /// </summary>
    /// <param name="fromNode">スタート地点</param>
    /// <param name="toNode">目的地</param>
    /// <returns>コスト</returns>
    private float GetCostNodeToNode(Node fromNode, Node toNode)
    {
        float nodeDistance = Vector3.Distance(fromNode.Position, toNode.Position);
        return nodeDistance;
    }

    private List<Node> ReconstructPath(Dictionary<Vector3, Vector3> cameFrom, Node startNode, Node endNode)
    {
        List<Node> path = new List<Node> { endNode };
        Vector3 currentNode = endNode.Position;

        //スタートのまでの経路を逆順で取得
        while (currentNode != startNode.Position)
        {
            //道のりに格納
            path.Add(new Node(currentNode));

            //次のノードを取得
            currentNode = cameFrom[currentNode];
        }

        path.Add(startNode);
        path.Reverse();
        return path;
    }

    /// <summary>
    /// 隣接ノードを取得
    /// </summary>
    /// <param name="node">中心のノード</param>
    /// <returns></returns>
    IEnumerable<Node> GetNeighbors(Node node)
    {
        Vector3[] direction = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        return direction
            .Select(direction => _gridGeneratePresenter.GetNodeWorldPosition(node.Position + direction * _gridGeneratePresenter.GridCellSize))
            .Where(neighbor => neighbor.IsWalkable);
    }
}
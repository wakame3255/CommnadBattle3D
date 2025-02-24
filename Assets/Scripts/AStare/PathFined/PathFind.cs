using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// �o�H�T���i���[�u�n�̔z���ɂ���ׂ��H�j
/// </summary>
public class PathFind
{
    private GridGeneratePresenter _gridGeneratePresenter;

    public List<Node> ReturnFindTacticalPath(Node startNod, Node endNode)
    {
        //�I���������̗D��x��\��
        PriorityQueue<Node, float> openList = new PriorityQueue<Node, float>();

        //�����Ȃ������ĕ]�����Ȃ����߂̃��X�g
        HashSet<Vector3> closedSet = new HashSet<Vector3>();

        //�ǂ�����ǂ��ɗ��������L�^���郊�X�g
        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();

        //�X�^�[�g����̃R�X�g���L�^���郊�X�g
        Dictionary<Vector3, float> costSoFar = new Dictionary<Vector3, float> { [startNod.Position] = 0};

        //�X�^�[�g����̘I�o�x���L�^���郊�X�g
        Dictionary<Vector3, float> exposureSoFar = new Dictionary<Vector3, float> { [startNod.Position] = 0};


        //�X�^�[�g�n�_��ǉ�
        openList.Enqueue(startNod, 0);

       while (openList.Count > 0)
        {
            //��ԃR�X�g���Ⴂ�m�[�h���擾
            Node currentNode = openList.Dequeue();

            //�S�[���Ȃ̂��̊m�F
            if (currentNode.Position == endNode.Position)
            {
                //return ReconstructPath(cameFrom, currentNode);
            }

            //�]���ς݂ɒǉ�
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
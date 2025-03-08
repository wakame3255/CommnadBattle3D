using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// �o�H�T���i���[�u�n�̔z���ɂ���ׂ��H�j
/// </summary>
public class PathFind : MonoBehaviour
{
    [SerializeField]
    private GridGeneratePresenter _gridGeneratePresenter;

    [SerializeField]
    private float _possibleHeight;

    public List<Node> ReturnFindTacticalPath(Node startNode, Node endNode)
    {
        //�I���������̗D��x��\��
        PriorityQueue<Node, float> openList = new PriorityQueue<Node, float>();

        //�����Ȃ������ĕ]�����Ȃ����߂̃��X�g
        HashSet<Vector3> closedSet = new HashSet<Vector3>();

        //�ǂ�����ǂ��ɗ��������L�^���郊�X�g
        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();

        //�X�^�[�g����̃R�X�g���L�^���郊�X�g
        Dictionary<Vector3, float> costSoFar = new Dictionary<Vector3, float> { [startNode.Position] = 0};

        //�X�^�[�g����̘I�o�x���L�^���郊�X�g
        Dictionary<Vector3, float> exposureSoFar = new Dictionary<Vector3, float> { [startNode.Position] = 0};


        //�X�^�[�g�n�_��ǉ�
        openList.Enqueue(startNode, 0);

       while (openList.Count > 0)
        {
            //��ԃR�X�g���Ⴂ�m�[�h���擾
            Node currentNode = openList.Dequeue();

            //�S�[���Ȃ̂��̊m�F
            if (currentNode.Position == endNode.Position)
            {
                return ReconstructPath(cameFrom, startNode, endNode);
            }

            //�]���ς݂ɒǉ�
            closedSet.Add(currentNode.Position);


            //�אڃm�[�h���擾
            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                //�]���ς݂̃m�[�h�Ɠn��Ȃ��m�[�h�̓X�L�b�v
                if (closedSet.Contains(neighbor.Position) || Mathf.Abs(neighbor.Position.y - currentNode.Position.y) > _possibleHeight)
                {
                    continue;
                }
               
                //�R�X�g�̌v�Z
                float newCost = costSoFar[currentNode.Position] + GetCostNodeToNode(currentNode, neighbor);

                //�ŏ��R�X�g�ŊJ����΍ĒT��
                if (costSoFar.TryGetValue(neighbor.Position, out float existingCost) && newCost >= existingCost)
                {
                    continue;
                }

                //�ŏ��R�X�g�̍X�V
                costSoFar[neighbor.Position] = newCost;

                //�ǂ̃m�[�h����ǂ̃m�[�h�ɗ��������L�^
                cameFrom[neighbor.Position] = currentNode.Position;

                //�m�[�h�Ԃ̈ړ��R�X�g�ƃS�[���܂ł̋������������ėD��x���v�Z
                float priority = newCost + GetCostNodeToNode(neighbor, endNode);

                //�S�[���ɋ߂��̃m�[�h�Ƃ��Ēǉ�
                openList.Enqueue(neighbor, priority);
            }
        }
       Debug.Log("�o�H��������܂���ł���");

        return null;
    }

    public Node GetNodeWorldPosition(Vector3 worldPosition)
    {
        return _gridGeneratePresenter.GetNodeWorldPosition(worldPosition);
    }

    /// <summary>
    /// �m�[�h�Ԃ̃R�X�g���擾
    /// </summary>
    /// <param name="fromNode">�X�^�[�g�n�_</param>
    /// <param name="toNode">�ړI�n</param>
    /// <returns>�R�X�g</returns>
    private float GetCostNodeToNode(Node fromNode, Node toNode)
    {
        float nodeDistance = Vector3.Distance(fromNode.Position, toNode.Position);
        return nodeDistance;
    }

    /// <summary>
    /// �o�H���܂Ƃ߂�
    /// </summary>
    /// <param name="cameFrom">�m�[�h�Ԃ̓��̂莫��</param>
    /// <param name="startNode">�J�n�m�[�h</param>
    /// <param name="endNode">�ŏI�m�[�h</param>
    /// <returns>�o�H</returns>
    private List<Node> ReconstructPath(Dictionary<Vector3, Vector3> cameFrom, Node startNode, Node endNode)
    {
        List<Node> path = new List<Node> { endNode };
        Vector3 currentNode = endNode.Position;

        //�X�^�[�g�̂܂ł̌o�H���t���Ŏ擾
        while (currentNode != startNode.Position)
        {
            //���̂�Ɋi�[
            path.Add(new Node(currentNode));

            //���̃m�[�h���擾
            currentNode = cameFrom[currentNode];
        }

        //�Ō�ɃX�^�[�g�n�_�̒ǉ�
        path.Add(startNode);

        //�o�H�𔽓]�����ĕԂ�
        path.Reverse();

        //�o�H�̐F��ύX
        _gridGeneratePresenter.ChangeViewColorNode(path);
        return path;
    }

    /// <summary>
    /// �אڃm�[�h���擾
    /// </summary>
    /// <param name="node">���S�̃m�[�h</param>
    /// <returns></returns>
    IEnumerable<Node> GetNeighbors(Node node)
    {
        Vector3[] direction = 
            { Vector3.forward, Vector3.back, Vector3.left, Vector3.right, Vector3.forward + Vector3.left, Vector3.forward + Vector3.right, Vector3.back + Vector3.left, Vector3.back + Vector3.right };
        return direction
            .Select(direction => _gridGeneratePresenter.GetNodeWorldPosition(node.Position + direction * _gridGeneratePresenter.GridCellSize))
            .Where(neighbor => neighbor.IsWalkable);
    }
}
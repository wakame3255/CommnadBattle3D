
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

public class ActionHighlightsView : MonoBehaviour
{
    [SerializeField, Required]
    private GameObject _highlightPrefab;

    [SerializeField, Required]
    private GameObject _selectHighlightPrefab;

    List<GameObject> _highlights = new List<GameObject>();

    /// <summary>
    /// ターゲットの頭上にハイライトの生成
    /// </summary>
    /// <param name="targets"></param>
    public void InstanceHighlight(List<SelectionTargetData> targets)
    {
        //ハイライトの初期化
        DebugUtility.Log("poolにして");
        foreach (GameObject highlight in _highlights)
        {
            Destroy(highlight);
        }

        if (targets == null)
        {
            return;
        }

        //ターゲットの頭上にハイライトを生成
        foreach (SelectionTargetData target in targets)
        {
            GameObject highlight = null;

            if (target.IsSelected)
            {
                highlight = Instantiate(_selectHighlightPrefab, target.Collider.transform.position + (Vector3.up * 3), Quaternion.identity);
            }
            else
            {
                highlight = Instantiate(_highlightPrefab, target.Collider.transform.position + (Vector3.up * 3), Quaternion.identity);
            }
            
            highlight.transform.SetParent(target.Collider.transform);
            _highlights.Add(highlight);
        }
    }
}

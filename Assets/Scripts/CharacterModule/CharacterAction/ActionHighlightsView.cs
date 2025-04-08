
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

public class ActionHighlightsView : MonoBehaviour
{
    [SerializeField, Required]
    private GameObject _highlightPrefab;

    List<GameObject> _highlights = new List<GameObject>();

    /// <summary>
    /// ターゲットの頭上にハイライトの生成
    /// </summary>
    /// <param name="targets"></param>
    public void InstanceHighlight(List<Collider> targets)
    {
        //ハイライトの初期化
        DebugUtility.Log("poolにして");
        foreach (var highlight in _highlights)
        {
            Destroy(highlight);
        }

        if (targets == null)
        {
            return;
        }

        foreach (var target in targets)
        {
            var highlight = Instantiate(_highlightPrefab, target.transform.position + (Vector3.up * 3), Quaternion.identity);
            highlight.transform.SetParent(target.transform);
            _highlights.Add(highlight);
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラクターのステータスを表示するビュー
/// HP、移動距離、行動コストなどの情報をUIに表示
/// </summary>
public class CharacterStatusView : MonoBehaviour, IInitialize
{
    [SerializeField, Required]
    private Text _healthText;

    [SerializeField, Required]
    private Text _travelDistanceText;

    [SerializeField, Required]
    private Text _actionCost;

    //行動コスト

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        
    }

    /// <summary>
    /// キャラクターの状態に応じてUIの表示/非表示を切り替え
    /// </summary>
    /// <param name="state">キャラクターの状態</param>
    public void SetCharacterState(CharacterState state)
    {
        switch(state)
        {
            case CharacterState.Stay:
                break;
            case CharacterState.Move:
                ShowUI();
                break;
            case CharacterState.End:
                HideUI();
                break;
        }
    }

    /// <summary>
    /// 残り移動距離をテキストで表示
    /// </summary>
    /// <param name="distance">残り移動距離</param>
    public void SetTravelDistance(float distance)
    {
        if (_travelDistanceText == null)
        {
            return;
        }

        _travelDistanceText.text = "残り移動距離 : " + distance.ToString();
    }

    /// <summary>
    /// 行動コストをテキストで表示
    /// </summary>
    /// <param name="actionCost">行動コスト</param>
    public void SetActionCost(int actionCost)
    {
        if (_actionCost == null)
        {
            return;
        }
        _actionCost.text = "行動コスト : " + actionCost.ToString();
    }

    /// <summary>
    /// HPをテキストで表示
    /// </summary>
    /// <param name="health">現在のHP</param>
    public void SetHealth(int health)
    {
        if (_healthText == null)
        {
            return;
        }
        _healthText.text = "HP : " + health.ToString();
    }

    /// <summary>
    /// UIを表示
    /// </summary>
    private void ShowUI()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// UIを非表示
    /// </summary>
    private void HideUI()
    {
        gameObject.SetActive(false);
    }
}

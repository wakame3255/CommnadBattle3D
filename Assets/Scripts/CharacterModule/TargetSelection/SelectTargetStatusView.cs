using System;
using System.ComponentModel.DataAnnotations;
using UnityEngine;
using UnityEngine.UI;

public class SelectTargetStatusView : MonoBehaviour
{
    [SerializeField, Required]
    private Text _targetNameText = default;

    [SerializeField, Required]
    private Image _targetImage = default;

    [SerializeField, Required]
    private Text _targetHpText = default;

    [SerializeField, Required]
    private Text _targetAttackText = default;

    /// <summary>
    /// キャラクターの情報を挿入
    /// </summary>
    /// <param name="hp"></param>
    public void SetCharacterInfomation(CharacterStatusModel statusModel)
    {
        if (statusModel == null)
        {
            return;
        }

        string targetName = statusModel.RPHealth.CurrentValue.ToString();

        _targetNameText.text = "名前：" + targetName;
        _targetHpText.text = "HP：" + targetName;
        _targetAttackText.text = "攻撃力：" + targetName;
        _targetImage.sprite = null;

        DebugUtility.Log("テスト用メソッド");
    }
}

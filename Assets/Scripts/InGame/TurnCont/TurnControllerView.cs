using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ターン制御のビュー
/// キャラクター選択ボタンなどのUI表示を管理
/// </summary>
public class TurnControllerView : MonoBehaviour, IInitialize
{
    [SerializeField, Required]
    private Button _CharacterButton = default;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
       
    }

    /// <summary>
    /// ビューを更新
    /// キャラクター分のボタンを生成
    /// </summary>
    /// <param name="characterStateHandlers">キャラクター状態管理リスト</param>
    public void UpdateView(List<ICharacterStateController> characterStateHandlers)
    {
        for (int i = 0; i < characterStateHandlers.Count; i++)
        {
            Instantiate(_CharacterButton, this.transform);
        }
    }
}

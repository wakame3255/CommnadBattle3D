using System;
using UnityEngine;

/// <summary>
/// インゲームのビュー
/// ゲーム状態に応じたUI表示を管理
/// </summary>
public class InGameView : MonoBehaviour, IInitialize
{
    private GameState cullentState;

    /// <summary>
    /// ビューを更新
    /// ゲーム状態に応じた表示に切り替え
    /// </summary>
    /// <param name="state">現在のゲーム状態</param>
    public void UpdateView(GameState state)
    {
        cullentState = state;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
       
    }
}
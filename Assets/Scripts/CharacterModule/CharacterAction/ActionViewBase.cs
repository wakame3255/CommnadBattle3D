using System;
using UnityEngine;
using UnityEngine.UI;
using R3;

/// <summary>
/// アクションビューの基底クラス
/// 全てのアクションビューが継承する共通機能を提供
/// </summary>
public abstract class ActionViewBase : MonoBehaviour, IInitialize
{
    [SerializeField] 
    protected Button _actionButton;

    /// <summary>
    /// アクションを実行するボタン
    /// </summary>
    public Button ActionButton => _actionButton;

    protected ActionControllerView _actionContView;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        
    }

    /// <summary>
    /// アクションコントローラービューを設定
    /// </summary>
    /// <param name="actionCont">設定するアクションコントローラービュー</param>
    public void SetActionContView(ActionControllerView actionCont)
    {
        _actionContView = actionCont;
    }
}
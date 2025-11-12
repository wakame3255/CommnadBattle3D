using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

/// <summary>
/// アクションモデルを設定するインターフェース
/// キャラクターの行動とその対象を設定
/// </summary>
public interface ISetActionModel
{
    /// <summary>
    /// キャラクターの現在位置
    /// </summary>
    public Vector3 CharacterPos { get;}

    /// <summary>
    /// アクションモデルを設定
    /// </summary>
    /// <param name="characterAction">設定するアクションモデル</param>
    public void SetActionModel(ActionModelBase characterAction);

    /// <summary>
    /// アクションの対象範囲を設定
    /// </summary>
    /// <param name="targets">対象となるコライダーのリスト</param>
    public void SetScopeTarget(List<Collider> targets);
}

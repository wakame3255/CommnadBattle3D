using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapActionBase : MonoBehaviour
{
    /// <summary>
    /// アクションの前提条件　満たせば実行可能
    /// </summary>
    public Dictionary<WorldStateKey, object> Preconditions { get; protected set; } = new Dictionary<WorldStateKey, object>();

    /// <summary>
    /// アクションの効果　実行後の状態
    /// </summary>
    public Dictionary<WorldStateKey, object> Effects { get; protected set; } = new Dictionary<WorldStateKey, object>();

    /// <summary>
    /// 実行コスト
    /// </summary>
    public float Cost { get; protected set; } = 1f;

    /// <summary>
    /// アクションの名前
    /// </summary>
    public string ActionName { get; protected set; } = "ActionName";

    /// <summary>
    /// テスト用の有効フラグ
    /// </summary>
    public bool IsEnable { get; protected set; } = true;

    /// <summary>
    /// 初期化メソッド
    /// </summary>
    public abstract void Setup();

    /// <summary>
    /// アクションの完了
    /// </summary>
    /// <returns></returns>
    public virtual bool IsActionDone()
    {
        return true;
    }

    /// <summary>
    /// アクションの実行前の確認
    /// </summary>
    /// <param name="agent"></param>
    /// <returns></returns>
    public virtual bool CheckProcedures(GameObject agent)
    {
        return true;
    }

    /// <summary>
    /// アクションの実行 合否を返す
    /// </summary>
    /// <param name="agent"></param>
    /// <param name="woldState"></param>
    /// <returns></returns>
    public abstract bool Parform(GameObject agent, Dictionary<WorldStateKey, object> woldState);

    /// <summary>
    /// アクションの実行後の処理 リセット処理
    /// </summary>
    public virtual void Reset() { }
}

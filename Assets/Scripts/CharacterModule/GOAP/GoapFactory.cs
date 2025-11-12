using System;
using UnityEngine;

/// <summary>
/// GOAPプランナーのファクトリークラス
/// GOAPプランナーインスタンスを生成
/// </summary>
public class GoapFactory : MonoBehaviour
{
    /// <summary>
    /// GOAPプランナーを生成
    /// </summary>
    /// <returns>生成されたGOAPプランナー</returns>
    public IGoapPlanner CreateGoapPlanner()
    {
        return new GoapPlanner();
    }
}

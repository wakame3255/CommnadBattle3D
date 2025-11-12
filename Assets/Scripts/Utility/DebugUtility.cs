
using UnityEngine;

/// <summary>
/// デバッグ用のユーティリティクラス
/// Unity のログ機能をラップして使いやすくする
/// </summary>
public class DebugUtility 
{
    /// <summary>
    /// デバッグメッセージをコンソールに出力
    /// </summary>
    /// <param name="message">出力するメッセージ</param>
    public static void Log(string message)
    {
        Debug.Log(message);
    }
}

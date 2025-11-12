using Unity.VisualScripting;

/// <summary>
/// Vector3型の拡張メソッドを提供するクラス
/// UnityEngine.Vector3とSystem.Numerics.Vector3の相互変換機能
/// </summary>
public static class Vector3Extensions
{
    /// <summary>
    /// System.Numerics.Vector3をUnityEngine.Vector3に変換
    /// </summary>
    /// <param name="vector">変換元のSystem.Numerics.Vector3</param>
    /// <returns>変換されたUnityEngine.Vector3</returns>
    public static UnityEngine.Vector3 ToUnityVector3(this System.Numerics.Vector3 vector)
    {
        UnityEngine.Vector3 pos = default;
        pos.Set(vector.X, vector.Y, vector.Z);
        return pos;
    }

    /// <summary>
    /// UnityEngine.Vector3をSystem.Numerics.Vector3に変換
    /// </summary>
    /// <param name="vector">変換元のUnityEngine.Vector3</param>
    /// <returns>変換されたSystem.Numerics.Vector3</returns>
    public static System.Numerics.Vector3 ToSystemVector3(this UnityEngine.Vector3 vector)
    {
        System.Numerics.Vector3 pos = new System.Numerics.Vector3(vector.x, vector.y, vector.z);
        return pos;
    }
}


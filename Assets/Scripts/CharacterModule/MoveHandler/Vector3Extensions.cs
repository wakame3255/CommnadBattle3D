using Unity.VisualScripting;

public static class Vector3Extensions
{
    public static UnityEngine.Vector3 ToUnityVector3(this System.Numerics.Vector3 vector)
    {
        UnityEngine.Vector3 pos = default;
        pos.Set(vector.X, vector.Y, vector.Z);
        return pos;
    }

    public static System.Numerics.Vector3 ToSystemVector3(this UnityEngine.Vector3 vector)
    {
        System.Numerics.Vector3 pos = new System.Numerics.Vector3(vector.x, vector.y, vector.z);
        return pos;
    }
}


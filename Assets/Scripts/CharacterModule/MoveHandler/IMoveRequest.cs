using System;

public interface IMoveRequest
{
    public void MovePosition(System.Numerics.Vector3 pos);

    public void MoveStop();

    public void Updateable();
}

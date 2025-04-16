using System;
using UnityEngine;

public interface IActionStrategy
{
    public bool CanPerform { get; }
    public bool Complete { get; }

   public void Enter() { }

    public void Update(float deltaTime) { }

    public void End() { }
}
public class AttackStrategy : IActionStrategy
{
    public bool CanPerform => true;
    public bool Complete { get; private set; }

    readonly CountdownTimer _timer;

    public AttackStrategy()
    {
        //アニメーションの情報をもらう
    }

    public void Enter()
    {
        throw new NotImplementedException();
    }
    public void Update(float deltaTime)
    {
        throw new NotImplementedException();
    }
    public void End()
    {
        throw new NotImplementedException();
    }
}

public class MoveStrategy : IActionStrategy
{
    readonly IMoveRequest _moveModel;
    readonly Func<Vector3> _destination;

    public bool CanPerform => !Complete;
    public bool Complete => true; //目的地までの距離による
    public MoveStrategy(MoveModel moveModel, Func<Vector3> destination)
    {
        this._moveModel = moveModel;
        this._destination = destination;
    }

    public void Enter() => _moveModel.MovePosition(Vector3Extensions.ToSystemVector3(_destination()));

    public void End() => _moveModel.MoveStop();
   
}

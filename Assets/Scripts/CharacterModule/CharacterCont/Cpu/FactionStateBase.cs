using System;

public abstract class FactionStateBase
{
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();
}

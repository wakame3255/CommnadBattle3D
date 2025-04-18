using System;

public abstract class FactionStateBase
{
    protected AllCharacterStatus _allCharacterStatus;

    protected IChangeActionRequest _request;

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();
}

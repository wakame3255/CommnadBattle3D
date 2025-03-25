using System;
using R3;
public interface IActionNotice
{
    public ReadOnlyReactiveProperty<int> RPActionCost { get; }

    public void NotifyUseActionCost(int actionCost);
}
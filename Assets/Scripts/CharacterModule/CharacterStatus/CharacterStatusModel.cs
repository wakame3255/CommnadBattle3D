using System;
using R3;

public class CharacterStatusModel : IInitialize, IMoveNotice
{
    /// <summary>
    /// 残りの移動距離
    /// </summary>
    private ReactiveProperty<float> _travelDistance;
    public ReadOnlyReactiveProperty<float> TravelDistance { get => _travelDistance; }

    /// <summary>
    /// 状態のリセット
    /// </summary>
    public void ResetStatus()
    {

    }

    public void NotifyMove(float moveDistance)
    {

    }
    public void Initialize()
    {
        throw new NotImplementedException();
    }
}
using System;
using R3;

public class CharacterStatusModel : IInitialize, IMoveNotice
{
    /// <summary>
    /// 残りの移動距離
    /// </summary>
    private ReactiveProperty<float> _travelDistance;
    public ReadOnlyReactiveProperty<float> TravelDistance { get => _travelDistance; }

    //プレイヤーの状態
    private ReactiveProperty<CharacterState> _rPCurrentState;
    //プレイヤーの状態公開
    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get => _rPCurrentState; }

    /// <summary>
    /// キャラクターの状態ハンドラ
    /// </summary>
    private ICharacterStateHandler _stateHandler;

    public CharacterStatusModel(ICharacterStateHandler stateHandler)
    {
        _stateHandler = stateHandler;
    }

    public void Initialize()
    {
        _travelDistance = new ReactiveProperty<float>(0);

        _rPCurrentState = new ReactiveProperty<CharacterState>();

        _stateHandler.RPCurrentState.Subscribe(ChangeState);
    }

    /// <summary>
    /// 状態のリセット
    /// </summary>
    public void ResetStatus()
    {

    }

    public void NotifyMove(float moveDistance)
    {

    }

    /// <summary>
    /// 状態の変更イベント
    /// </summary>
    /// <param name="state"></param>
    private void ChangeState(CharacterState state)
    {
        _rPCurrentState.Value = state;

        switch (state)
        {
            case CharacterState.Stay:
                break;
            case CharacterState.Move:
                ResetStatus();
                break;
            case CharacterState.End:
                break;
        }
    }
}
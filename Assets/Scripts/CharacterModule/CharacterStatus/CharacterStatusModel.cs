using System;
using R3;

public class CharacterStatusModel : IInitialize, IMoveNotice, IActionNotice, IDamageNotice
{
    /// <summary>
    /// 残りの移動距離
    /// </summary>
    private ReactiveProperty<float> _travelDistance;
    public ReadOnlyReactiveProperty<float> TravelDistance { get => _travelDistance; }

    /// <summary>
    /// 行動コスト
    /// </summary>
    private ReactiveProperty<int> _rPActionCost;
    public ReadOnlyReactiveProperty<int> RPActionCost { get => _rPActionCost; }

    /// <summary>
    /// キャラクターのHP
    /// </summary>
    private ReactiveProperty<int> _rPHealth;
    public ReadOnlyReactiveProperty<int> RPHealth { get => _rPHealth; }
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

        _rPActionCost = new ReactiveProperty<int>(0);

        _rPHealth = new ReactiveProperty<int>(10);

        _rPCurrentState = new ReactiveProperty<CharacterState>();

        _stateHandler.RPCurrentState.Subscribe(ChangeState);
    }

    /// <summary>
    /// 状態のリセット
    /// </summary>
    public void ResetStatus()
    {
        _travelDistance.Value = 10;
        _rPActionCost.Value = 2;
    }

    /// <summary>
    /// 移動距離の通知
    /// </summary>
    /// <param name="moveDistance"></param>
    public void NotifyMove(float moveDistance)
    {
        _travelDistance.Value -= moveDistance;

        //ゼロリセット
        if (_travelDistance.Value < 0)
        {
            _travelDistance.Value = 0;
        }
    }

    /// <summary>
    /// 行動コストの通知
    /// </summary>
    /// <param name="actionCost"></param>
    public void NotifyUseActionCost(int actionCost)
    {
        _rPActionCost.Value -= actionCost;
    }

    /// <summary>
    /// ダメージ通知
    /// </summary>
    /// <param name="damage"></param>
    public void NotifyDamage(int damage)
    {
        _rPHealth.Value -= damage;
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
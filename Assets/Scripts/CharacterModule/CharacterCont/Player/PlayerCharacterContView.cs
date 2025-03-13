using R3;

public class PlayerCharacterContView : IInitialize
{
    private ReactiveProperty<bool> _rPEndTurn;

    public ReadOnlyReactiveProperty<bool> RPEndTurn { get => _rPEndTurn; }

    public void Initialize()
    {
        _rPEndTurn = new ReactiveProperty<bool>(false);
    }

    /// <summary>
    /// モデルの状態に応じてビューを更新する
    /// </summary>
    /// <param name="state">状態</param>
    public void UpdateView(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Stay:
                break;
            case CharacterState.Move:
                break;
            case CharacterState.End:
                break;
        }
    }
}
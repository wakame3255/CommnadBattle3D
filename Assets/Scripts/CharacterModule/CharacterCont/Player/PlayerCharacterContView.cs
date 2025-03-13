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
    /// ���f���̏�Ԃɉ����ăr���[���X�V����
    /// </summary>
    /// <param name="state">���</param>
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
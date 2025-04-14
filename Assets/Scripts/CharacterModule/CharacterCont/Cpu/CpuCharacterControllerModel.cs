using R3;

public class CpuCharacterControllerModel : ICharacterStateController
{
    private ReactiveProperty<CharacterState> _currentState;
    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get => _currentState; }

    private FactionStateController _factionStateController;

    public CpuCharacterControllerModel()
    {
        _currentState = new ReactiveProperty<CharacterState>(CharacterState.Stay);
    }

    public void ChangeCharacterState(CharacterState characterState)
    {
        _currentState.Value = characterState;
    }

    /// <summary>
    /// CPUに必要な情報をセットする
    /// </summary>
    /// <param name="otherCharacterStatus"></param>
    /// <param name="cpuBaseAction"></param>
    public void SetCpuInfomation(AllCharacterStatus otherCharacterStatus, CpuBaseActionInformation cpuBaseAction)
    {
        new FactionStateController(this, cpuBaseAction);
    }
}

public class CpuBaseActionInformation
{
    /// <summary>
    /// 細かい行動を参照するためのデータ
    /// </summary>
    private CpuActionControllerModel _actionControllerModel;
    public CpuActionControllerModel ActionControllerModel => _actionControllerModel;

    /// <summary>
    /// 全てのキャラクターの状態を参照するためのデータ
    /// </summary>
    private AllCharacterStatus _allCharacterStatus;
    public AllCharacterStatus AllCharacterStatus => _allCharacterStatus;

    /// <summary>
    /// 移動のリクエストを行うためのデータ
    /// </summary>
    private IMoveRequest _moveReqest;
    public IMoveRequest MoveReqest => _moveReqest;

    public CpuBaseActionInformation(CpuActionControllerModel actionControllerModel, IMoveRequest moveReqest, AllCharacterStatus allCharacterStatus)
    {
        _actionControllerModel = actionControllerModel;
        _moveReqest = moveReqest;
    }
}

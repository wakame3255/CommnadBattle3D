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
    public void SetCpuInformation(AllCharacterStatus otherCharacterStatus, CpuBaseActionInformation cpuBaseAction)
    {
        new FactionStateController(this, cpuBaseAction, otherCharacterStatus);
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
    /// 移動のリクエストを行うためのデータ
    /// </summary>
    private IMoveRequest _moveRequest;
    public IMoveRequest MoveReqest => _moveRequest;

    public CpuBaseActionInformation(CpuActionControllerModel actionControllerModel, IMoveRequest moveReqest)
    {
        _actionControllerModel = actionControllerModel;
        _moveRequest = moveReqest;
    }
}

public class CpuActionInformation
{
    public CpuCharacterControllerModel ControllerModel { get; }
    public CharacterStatusModel StatusModel { get; }
    public CpuActionControllerModel ActionModel {  get;}
    public IMoveRequest MoveReqest { get; }

    public CpuActionInformation(CpuCharacterControllerModel controllerModel, CpuActionControllerModel actionControllerModel, IMoveRequest moveReqest, CharacterStatusModel statusModel)
    {
        ControllerModel = controllerModel;
        ActionModel = actionControllerModel;
        MoveReqest = moveReqest;
        StatusModel = statusModel;
    }
}



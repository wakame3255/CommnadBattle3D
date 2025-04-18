using System;
using R3;
using Cysharp.Threading.Tasks;
public class FactionStateController : IUpdateHandler, IChangeActionRequest
{
   private CpuCharacterControllerModel _cpuCharacter;

    private AllCharacterStatus _allCharacterStatus;

    private FactionStateBase _currentAction;

    private CharacterState _currentState;

    public MoveAction MoveActionState { get; }
    public AttackActionState AttackActionState { get; }

    public FactionStateController(CpuCharacterControllerModel cpuCharacterCont, CpuBaseActionInformation baseActionInformation, AllCharacterStatus allCharacterStatus)
    {
        _cpuCharacter = cpuCharacterCont;

        _allCharacterStatus = allCharacterStatus;
        MoveActionState = new MoveAction(baseActionInformation.MoveReqest, allCharacterStatus, this);
        AttackActionState = new AttackActionState(baseActionInformation.ActionControllerModel, allCharacterStatus, this);

        SetEvent();
    }

    public void NoticeEnd()
    {
        _cpuCharacter.ChangeCharacterState(CharacterState.End);
       
    }

    public void Updateable()
    {
        if (_currentAction != null || _currentState == CharacterState.Move)
        {
            _currentAction.UpdateState();
        }  
    }

    public void ChangeActionRequest(FactionStateBase factionState)
    {
        _currentAction.ExitState();
        EnterAction(factionState);
    }

    private void EnterAction(FactionStateBase factionState)
    {
        _currentAction = factionState;
        _currentAction.EnterState();
    }

    private void SetEvent()
    {
        _cpuCharacter.RPCurrentState.Subscribe(SetChangeState);
    }

    /// <summary>
    /// キャラクターの状態の変更検知
    /// </summary>
    /// <param name="characterState">キャラクターの状態</param>
    private void SetChangeState(CharacterState characterState)
    {
        _currentState = characterState;

        switch (characterState)
        {
            case CharacterState.Stay:
                break;
            case CharacterState.Move:
                EnterAction(MoveActionState);
                break;
            case CharacterState.End:
                break;
        }
    }
}

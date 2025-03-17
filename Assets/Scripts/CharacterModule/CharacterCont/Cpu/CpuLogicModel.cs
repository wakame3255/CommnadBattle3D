using System;
using R3;

public class CpuLogicModel
{
    CpuCharacterContModel _cpuCharacter;
    public CpuLogicModel(CpuCharacterContModel cpuCharacterCont)
    {
        _cpuCharacter = cpuCharacterCont;

        SetEvent();
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
        switch(characterState)
        {
            case CharacterState.Stay:
                break;
            case CharacterState.Move:
                StartThinkCPU();
                break;
            case CharacterState.End:
                break;
        }
    }

    /// <summary>
    /// CPUの思考を開始する
    /// </summary>
    private void StartThinkCPU()
    {
        SetChangeState(CharacterState.End);
    }
}
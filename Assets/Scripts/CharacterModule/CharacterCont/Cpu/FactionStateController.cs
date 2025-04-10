using System;
using R3;
using Cysharp.Threading.Tasks;
public class FactionStateController : IUpdateHandler, IChangeActionRequest
{
   private CpuCharacterControllerModel _cpuCharacter;

    private AllCharacterStatus _allCharacterStatus;

    public FactionStateController(CpuCharacterControllerModel cpuCharacterCont, CpuBaseActionInformation baseActionInformation)
    {
        _cpuCharacter = cpuCharacterCont;

        _allCharacterStatus = baseActionInformation.AllCharacterStatus;

        SetEvent();
    }

    public void Updateable()
    {
        // CPUの思考を更新する処理が必要な場合はここに記述
    }

    public void ChangeActionRequest()
    {
        // CPUの行動を変更するリクエストを受け取った場合の処理
        // 例えば、CPUが移動するなどの処理をここに記述
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
        switch (characterState)
        {
            case CharacterState.Stay:
                break;
            case CharacterState.Move:
                StartThinkCPU().Forget();
                break;
            case CharacterState.End:
                break;
        }
    }

    /// <summary>
    /// CPUの思考を開始する
    /// </summary>
    private async UniTask StartThinkCPU()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2));

        _cpuCharacter.ChangeCharacterState(CharacterState.End);

        DebugUtility.Log("CPUの思考を終了");
    }
}

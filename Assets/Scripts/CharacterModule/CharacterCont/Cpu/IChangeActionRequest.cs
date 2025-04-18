using System;

public interface IChangeActionRequest
{
    /// <summary>
    /// CPUの行動を変更するリクエストを受け取った場合の処理
    /// </summary>
    public void ChangeActionRequest(FactionStateBase factionState);

    public void NoticeEnd();

    public MoveAction MoveActionState { get; }

    public AttackActionState AttackActionState { get; }
}

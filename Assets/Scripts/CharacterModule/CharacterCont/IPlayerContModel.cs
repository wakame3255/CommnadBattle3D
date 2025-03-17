using R3;
using System;

public interface IPlayerContModel
{
    /// <summary>
    /// プレイヤーの状態管理
    /// </summary>
    public ReadOnlyReactiveProperty<CharacterState> RPCurrentState { get; }

    /// <summary>
    /// ターン終了を通知
    /// </summary>
    public void NoticeEndTurn();

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize();
}
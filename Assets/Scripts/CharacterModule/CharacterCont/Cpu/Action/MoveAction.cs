using System;
using UnityEngine;

public class MoveAction : FactionStateBase
{
    private IMoveRequest _moveRequest;
    private Vector3 _targetPosition;

    // 経過時間を追跡するフィールド
    private float _elapsedTime;

    public MoveAction(IMoveRequest moveRequest, AllCharacterStatus allCharacter, IChangeActionRequest request)
    {
        _allCharacterStatus = allCharacter;
        _moveRequest = moveRequest;
        _request = request;
    }

    public override void EnterState()
    {
        // 経過時間をリセット
        _elapsedTime = 0f;

        foreach (var characterStatus in _allCharacterStatus.AllyCharacterStatus)
        {
            DebugUtility.Log($"目的地の座標 : {characterStatus.NowPosition}");
        }
        _targetPosition = _allCharacterStatus.EnemyCharacterStatus[0].NowPosition;

        _moveRequest.MovePosition(Vector3Extensions.ToSystemVector3(_targetPosition));
    }

    public override void UpdateState()
    {
        // 経過時間を更新
        _elapsedTime += Time.deltaTime;

        // 5秒経過したら強制終了
        if (_elapsedTime >= 5f)
        {
            DebugUtility.Log("5秒経過したためMoveActionを終了します。");
            _request.ChangeActionRequest(_request.AttackActionState);
            _moveRequest.MoveStop();
            return;
        }

        // 目的地に到達した場合の処理
        if (Vector3.Distance(_allCharacterStatus.MyCharacterStatus.NowPosition, _targetPosition) < 1)
        {
            _request.ChangeActionRequest(_request.AttackActionState);
            _moveRequest.MoveStop();
        }

        _moveRequest.Updateable();
    }

    public override void ExitState()
    {
        // 状態終了時の処理（必要に応じて追加）
    }
}

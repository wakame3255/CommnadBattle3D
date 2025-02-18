
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class PlayerCharacter : MonoBehaviour
{
    private PlayerAction _playerAction;
    private PlayerStatus _playerStatus;

    [Inject]
    public void InJect(PlayerAction playerAction, PlayerStatus playerStatus)
    {
        _playerAction = playerAction;
        _playerStatus = playerStatus;

        DebugUtility.Log(_playerAction.ToString());
        DebugUtility.Log(_playerStatus.ToString());
    }
}

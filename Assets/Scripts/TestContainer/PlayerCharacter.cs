
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class PlayerCharacter : MonoBehaviour
{
    [Inject] private PlayerAction _playerAction;
    [Inject] private PlayerStatus _playerStatus;

    private void Start()
    {
        DebugUtility.Log(_playerAction.ToString());
        DebugUtility.Log(_playerStatus.ToString());
    }
}

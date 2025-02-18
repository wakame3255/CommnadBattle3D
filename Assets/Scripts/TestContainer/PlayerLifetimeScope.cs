
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerLifetimeScope : LifetimeScope
{
    [SerializeField]
    private PlayerCharacter _playerCharacter;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerAction>(Lifetime.Singleton);
        builder.Register<PlayerStatus>(Lifetime.Singleton);

        builder.RegisterComponent(_playerCharacter);
    }
}

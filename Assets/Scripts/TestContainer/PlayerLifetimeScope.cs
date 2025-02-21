using VContainer;
using VContainer.Unity;

public class PlayerLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerAction>(Lifetime.Singleton);
        builder.Register<PlayerStatus>(Lifetime.Singleton);
        builder.RegisterComponentInHierarchy<PlayerCharacter>();
    }
}

   
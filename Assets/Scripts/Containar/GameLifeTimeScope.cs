
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        //ƒ‚ƒfƒ‹‚Ì“o˜^
        builder.Register<InGameModel>(Lifetime.Singleton).As<IGameStateChanger, InGameModel>();
        builder.Register<TurnControllerModel>(Lifetime.Singleton);

        //ƒvƒŒƒ[ƒ“ƒ^[‚Ì“o˜^
        builder.Register<InGamePresenter>(Lifetime.Singleton);
        builder.Register<TurnControllerPresenter>(Lifetime.Singleton);

        //ƒrƒ…[‚Ì“o˜^
        builder.RegisterComponentInHierarchy<InGameView>();
        builder.RegisterComponentInHierarchy<TurnControllerView>();
        builder.RegisterComponentInHierarchy<CharacterGenerator>().As<ICharacterGenerator, CharacterGenerator>();

        //ƒXƒ^[ƒgƒAƒbƒv‚Ì“o˜^
        builder.RegisterEntryPoint<PresenterStartUp>(Lifetime.Singleton);
    }
}
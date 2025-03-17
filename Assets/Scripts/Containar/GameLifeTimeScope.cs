
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        //モデルの登録
        builder.Register<InGameModel>(Lifetime.Singleton).As<IGameStateChanger, InGameModel>();
        builder.Register<TurnControllerModel>(Lifetime.Singleton);
        builder.Register<PlayerCharacterContModel>(Lifetime.Singleton).As<IPlayerContModel>();

        //プレゼンターの登録
        builder.Register<InGamePresenter>(Lifetime.Singleton);
        builder.Register<TurnControllerPresenter>(Lifetime.Singleton);
        builder.Register<PlayerCharacterContPresenter>(Lifetime.Singleton);

        //ビューの登録
        builder.RegisterComponentInHierarchy<InGameView>();
        builder.RegisterComponentInHierarchy<TurnControllerView>();
        builder.RegisterComponentInHierarchy<PlayerCharacterContView>();
        builder.RegisterComponentInHierarchy<CharacterGenerator>().As<ICharacterGenerator, CharacterGenerator>();

        //スタートアップの登録
        builder.RegisterEntryPoint<PresenterStartUp>(Lifetime.Singleton);
    }
}
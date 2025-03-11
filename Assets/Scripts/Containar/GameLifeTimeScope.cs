
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        //���f���̓o�^
        builder.Register<InGameModel>(Lifetime.Singleton).As<IGameStateChanger, InGameModel>();
        builder.Register<TurnControllerModel>(Lifetime.Singleton);

        //�v���[���^�[�̓o�^
        builder.Register<InGamePresenter>(Lifetime.Singleton);
        builder.Register<TurnControllerPresenter>(Lifetime.Singleton);

        //�r���[�̓o�^
        builder.RegisterComponentInHierarchy<InGameView>();
        builder.RegisterComponentInHierarchy<TurnControllerView>();
        builder.RegisterComponentInHierarchy<CharacterGenerator>().As<ICharacterGenerator, CharacterGenerator>();

        //�X�^�[�g�A�b�v�̓o�^
        builder.RegisterEntryPoint<PresenterStartUp>(Lifetime.Singleton);
    }
}
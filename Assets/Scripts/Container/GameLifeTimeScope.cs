
using VContainer;
using VContainer.Unity;

/// <summary>
/// ゲーム全体の依存性注入コンテナを設定するクラス
/// モデル、プレゼンター、ビュー、および各種サービスを登録
/// </summary>
public class GameLifeTimeScope : LifetimeScope
{
    /// <summary>
    /// DIコンテナの設定
    /// ゲームで使用するすべてのコンポーネントを登録
    /// </summary>
    /// <param name="builder">DIコンテナビルダー</param>
    protected override void Configure(IContainerBuilder builder)
    {
        //モデルの登録
        builder.Register<InGameModel>(Lifetime.Singleton).As<IGameStateChanger, InGameModel>();
        builder.Register<TurnControllerModel>(Lifetime.Singleton);
        builder.Register<PlayerCharacterControllerModel>(Lifetime.Singleton).As<IPlayerContModel, PlayerCharacterControllerModel>();

        //プレゼンターの登録
        builder.Register<InGamePresenter>(Lifetime.Singleton);
        builder.Register<TurnControllerPresenter>(Lifetime.Singleton);
        builder.Register<PlayerCharacterContPresenter>(Lifetime.Singleton);

        //ビューの登録
        builder.RegisterComponentInHierarchy<InGameView>();
        builder.RegisterComponentInHierarchy<TurnControllerView>();
        builder.RegisterComponentInHierarchy<PlayerCharacterContView>();
        builder.RegisterComponentInHierarchy<CharacterStatusView>();
        builder.RegisterComponentInHierarchy<SelectTargetStatusView>();
        builder.RegisterComponentInHierarchy<ActionHighlightsView>();

        //キャラクター生成機能の登録
        builder.RegisterComponentInHierarchy<CharacterGenerator>().As<ICharacterGenerator, CharacterGenerator>();

        //プレイヤーが使用する入力情報
        builder.RegisterComponentInHierarchy<PlayerInput>().As<IInputInformation>();

        //すべてのキャラが共通で使用する機能
        builder.RegisterComponentInHierarchy<PathFind>();

        //スタートアップの登録
        builder.RegisterEntryPoint<PresenterStartUp>(Lifetime.Singleton);
    }

    
}

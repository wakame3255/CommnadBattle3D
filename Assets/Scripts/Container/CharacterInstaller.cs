
using VContainer;
using VContainer.Unity;

/// <summary>
/// キャラクター固有の依存性注入コンテナを設定するクラス
/// キャラクターごとに必要なコンポーネントを登録（現在は未使用）
/// </summary>
public class CharacterInstaller : LifetimeScope
{
    /// <summary>
    /// DIコンテナの設定
    /// </summary>
    /// <param name="builder">DIコンテナビルダー</param>
    protected override void Configure(IContainerBuilder builder)
    {
        
    }
}
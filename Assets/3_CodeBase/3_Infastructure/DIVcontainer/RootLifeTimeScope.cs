using UnityEngine;
using VContainer;
using VContainer.Unity;

public class RootLifeTimeScope : LifetimeScope
{
    [SerializeField] private DataCatalog _dataCatalog;
    [SerializeField] private PlayerData _playerData;

    protected override void Configure(IContainerBuilder builder)
    {
        
        builder.RegisterInstance(_dataCatalog);
        builder.RegisterInstance(_playerData);

        builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
    }
}

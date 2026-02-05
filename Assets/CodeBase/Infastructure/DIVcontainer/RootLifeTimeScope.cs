using Assets.CodeBase.Infastructure.Services;
using Core.Interfaces;
using Data;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class RootLifeTimeScope : LifetimeScope
    {
        [SerializeField] private GameplayRegistry _dataCatalog;
        [SerializeField] private PlayerData _playerData;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_dataCatalog);
            builder.RegisterInstance(_playerData);

            builder.Register<IAssetProviderService, AssetProviderService>(Lifetime.Singleton);
            builder.Register<SceneController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
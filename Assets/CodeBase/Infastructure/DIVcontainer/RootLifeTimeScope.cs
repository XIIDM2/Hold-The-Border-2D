using Assets.CodeBase.Infastructure.Services;
using Core.Interfaces;
using Data;
using Gameplay.UI;
using Infrastructure.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class RootLifeTimeScope : LifetimeScope
    {
        [SerializeField] private GameplayRegistry _dataCatalog;
        [SerializeField] private PlayerData _playerData;

        [SerializeField] private Fader _faderPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_dataCatalog);
            builder.RegisterInstance(_playerData);

            builder.RegisterInstance(_faderPrefab);

            builder.Register<IAssetProviderService, AssetProviderService>(Lifetime.Singleton);
            builder.Register<SceneController>(Lifetime.Singleton).AsSelf();
            builder.Register<AudioService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SettingsService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<EventBus>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
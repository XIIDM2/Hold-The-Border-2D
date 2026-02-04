using Data;
using Gameplay.UI;
using Infrastructure.Factories;
using Infrastructure.Managers;
using Infrastructure.Services;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class LevelLifeTimeScope : LifetimeScope
    {
        [SerializeField] private WaveData _waveData;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_waveData);

            builder.Register<IPlayerController, PlayerController>(Lifetime.Singleton);

            builder.Register<IWaveControllerService, WaveControllerService>(Lifetime.Singleton);
            builder.Register<ITowerBuildService, TowerBuildService>(Lifetime.Singleton);
            builder.Register<ITowerSelectionService, TowerSelectionService>(Lifetime.Singleton);

            builder.Register<IUnitFactory, UnitFactory>(Lifetime.Singleton);
            builder.Register<ITowerFactory, TowerFactory>(Lifetime.Singleton);
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<PathService>().As<IPathProvider>();

            builder.RegisterComponentInHierarchy<GameManager>();
        }
    }
}
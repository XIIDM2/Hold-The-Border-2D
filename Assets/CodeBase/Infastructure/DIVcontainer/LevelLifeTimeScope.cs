using Data;
using Infrastructure.Factories;
using Infrastructure.Managers;
using Infrastructure.Services;
using Infrastructure.Spawners;
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

            builder.Register<IPlayerController, PlayerController>(Lifetime.Scoped);

            builder.Register<IWaveService, WaveService>(Lifetime.Scoped);
            builder.Register<IUnitFactory, UnitFactory>(Lifetime.Scoped);
            builder.Register<ITowerFactory, TowerFactory>(Lifetime.Scoped);
            builder.Register<ITowerBuildService, TowerBuildService>(Lifetime.Scoped).As<IInitializable>().As<IDisposable>();

            builder.RegisterComponentInHierarchy<PathService>().As<IPathProvider>();

            builder.RegisterComponentInHierarchy<UnitSpawner>();

            builder.RegisterComponentInHierarchy<GameManager>();
        }
    }
}
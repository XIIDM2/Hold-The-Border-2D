using Data;
using Gameplay.Level;
using Gameplay.Path;
using Gameplay.Player;
using Infrastructure.Factories;
using Infrastructure.Services;
using Infrastructure.Services.Bootstrappers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class LevelLifeTimeScope : LifetimeScope
    {
        [SerializeField] private LevelsLabels _levelLabel;
        [SerializeField] private LevelData _LevelData;
        [SerializeField] private WaveData _waveData;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelLabel);
            builder.RegisterInstance(_LevelData);
            builder.RegisterInstance(_waveData);
            builder.RegisterComponentInHierarchy<LevelConfig>();

            builder.Register<IUnitFactory, UnitFactory>(Lifetime.Singleton);
            builder.Register<ITowerFactory, TowerFactory>(Lifetime.Singleton);

            builder.Register<PlayerController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<WaveControllerService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<TowerBuildService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TowerSelectionService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterComponentInHierarchy<RadiusVisualizerService>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<PathService>().As<IPathProvider>();

            builder.Register<LevelService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterInstance(destroyCancellationToken);

            builder.RegisterEntryPoint<LevelBootsTrapper>();
        }
    }
}
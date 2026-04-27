using Data;
using Gameplay.Path;
using Gameplay.Player;
using Infrastructure.Factories;
using Infrastructure.Managers;
using Infrastructure.Services;
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

            builder.Register<IUnitFactory, UnitFactory>(Lifetime.Singleton);
            builder.Register<ITowerFactory, TowerFactory>(Lifetime.Singleton);
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);

            builder.Register<PlayerController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<WaveControllerService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<ITowerBuildService, TowerBuildService>(Lifetime.Singleton);
            builder.Register<ITowerSelectionService, TowerSelectionService>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<PathService>().As<IPathProvider>();
            builder.RegisterComponentInHierarchy<RadiusVisualizerService>().AsImplementedInterfaces();

            builder.RegisterComponentInHierarchy<LevelManager>().AsImplementedInterfaces();

            builder.RegisterInstance(this.destroyCancellationToken);
        }
    }
}
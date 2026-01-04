using Data;
using Infrastructure.Factories;
using Infrastructure.Managers;
using Infrastructure.Spawners;
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

            builder.RegisterComponentInHierarchy<PathService>().As<IPathProvider>();

            builder.RegisterComponentInHierarchy<UnitSpawner>();
            builder.RegisterComponentInHierarchy<GameManager>();
        }
    }
}
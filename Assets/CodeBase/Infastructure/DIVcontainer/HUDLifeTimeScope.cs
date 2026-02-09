using Gameplay.UI;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class HUDLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PauseView>();
            builder.RegisterComponentInHierarchy<WavesView>();
            builder.RegisterComponentInHierarchy<PlayerStatsView>();
            builder.RegisterComponentInHierarchy<TowerBuildingView>();
            builder.RegisterComponentInHierarchy<TowerView>();

            builder.Register<PausePresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<WavesPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerStatsPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<TowerBuildingPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<TowerPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}
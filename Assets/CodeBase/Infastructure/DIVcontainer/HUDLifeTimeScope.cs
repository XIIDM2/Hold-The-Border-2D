using UnityEngine;
using VContainer;
using VContainer.Unity;

public class HUDLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<PlayerStatsUI>();
        builder.RegisterComponentInHierarchy<WavesUI>();
        builder.RegisterComponentInHierarchy<PauseUI>();
        builder.RegisterComponentInHierarchy<TowerBuildingControllerUI>();
        builder.RegisterComponentInHierarchy<TowerControllerUI>();
    }
}

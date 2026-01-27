using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using UnityEngine;
using UnityEngine.Events;

public interface ITowerSelectionService
{
    event UnityAction<BuildSite> BuildsiteClicked;
    event UnityAction<TowerController> TowerClicked;
    BuildSite BuildSite { get; }
    TowerController Tower {  get; }

    void SelectBuildSite(BuildSite site);   
    void SelectTower(TowerController tower);   
}

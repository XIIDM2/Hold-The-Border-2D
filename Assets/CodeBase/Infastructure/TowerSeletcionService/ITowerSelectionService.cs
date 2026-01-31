using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using UnityEngine;
using UnityEngine.Events;

public interface ITowerSelectionService
{
    event UnityAction<BuildSite> BuildsiteSelected;
    event UnityAction<TowerController> TowerSelected;

    event UnityAction BuildSiteDeselected;
    event UnityAction TowerDeselected;

    BuildSite BuildSite { get; }
    TowerController Tower {  get; }

    void SelectBuildSite(BuildSite site);   
    void SelectTower(TowerController tower);

    void ClearBuildSiteSelection();

    void ClearTowerSelection();
}

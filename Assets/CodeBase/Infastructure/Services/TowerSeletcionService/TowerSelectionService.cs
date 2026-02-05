using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using UnityEngine.Events;

namespace Infrastructure.Services
{
    public class TowerSelectionService : ITowerSelectionService
    {
        public event UnityAction<BuildSite> BuildsiteSelected;
        public event UnityAction<TowerController> TowerSelected;

        public event UnityAction BuildSiteDeselected;
        public event UnityAction TowerDeselected;
        public BuildSite BuildSite { get; private set; }
        public TowerController Tower { get; private set; }


        public void SelectBuildSite(BuildSite site)
        {
            BuildSite = site;
            BuildsiteSelected?.Invoke(BuildSite);
            ClearTowerSelection();
        }

        public void SelectTower(TowerController tower)
        {
            Tower = tower;
            TowerSelected?.Invoke(Tower);
            ClearBuildSiteSelection();

        }

        public void ClearBuildSiteSelection()
        {
            if (BuildSite == null) return;

            BuildSite = null;
            BuildSiteDeselected?.Invoke();
        }

        public void ClearTowerSelection()
        {
            if (Tower == null) return;

            Tower = null;
            TowerDeselected?.Invoke();
        }
    }
}
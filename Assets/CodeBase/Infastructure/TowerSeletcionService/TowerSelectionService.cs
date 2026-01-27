using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Services
{
    public class TowerSelectionService : ITowerSelectionService
    {
        public event UnityAction<BuildSite> BuildsiteClicked;
        public event UnityAction<TowerController> TowerClicked;
        public BuildSite BuildSite { get; private set; }
        public TowerController Tower { get; private set; }


        public void SelectBuildSite(BuildSite site)
        {
            BuildSite = site;
            BuildsiteClicked?.Invoke(BuildSite);
            Tower = null;
        }

        public void SelectTower(TowerController tower)
        {
            Tower = tower;
            TowerClicked?.Invoke(Tower);
            BuildSite = null;
        }
    }
}
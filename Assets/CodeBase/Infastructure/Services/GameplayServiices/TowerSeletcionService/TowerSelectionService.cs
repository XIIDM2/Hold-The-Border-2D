using Data;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using Infrastructure.Events;
using UnityEngine;
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

        private readonly IEventBus _eventBus;

        private readonly AudioClip _selectSound;

        public TowerSelectionService(IEventBus eventBus, SFXRegistry SFXRegistry)
        {
            _eventBus = eventBus;
            _selectSound = SFXRegistry.ClickSound;
        }

        public void SelectBuildSite(BuildSite site)
        {
            BuildSite = site;
            BuildsiteSelected?.Invoke(BuildSite);
            ClearTowerSelection();
            _eventBus.Publish(new InvokeSFX(_selectSound));
        }

        public void SelectTower(TowerController tower)
        {
            Tower = tower;
            TowerSelected?.Invoke(Tower);
            ClearBuildSiteSelection();
            _eventBus.Publish(new InvokeSFX(_selectSound));

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
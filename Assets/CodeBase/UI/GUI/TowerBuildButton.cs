using Gameplay.Towers;
using Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class TowerBuildButton : MonoBehaviour
    {
        private TowerType _type;

        private ITowerBuildService _buildService;
        private ITowerSelectionService _selectionService;

        [Inject]
        public void Construct(ITowerSelectionService selectionService, ITowerBuildService buildService)
        {
            _selectionService = selectionService;
            _buildService = buildService;
        }

        public void SetTowerType(TowerType type)
        {
            _type = type;
        }

        public void Build()
        {
            if (_selectionService.BuildSite) _buildService.BuildTower(_type, _selectionService.BuildSite);
            _selectionService.ClearBuildSiteSelection();
        }

    }
}
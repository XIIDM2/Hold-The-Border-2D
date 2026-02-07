using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Infrastructure.Managers;
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
        private LevelEntryPoint _manager;

        [Inject]
        public void Construct(ITowerSelectionService selectionService, ITowerBuildService buildService, LevelEntryPoint manager)
        {
            _selectionService = selectionService;
            _buildService = buildService;
            _manager = manager;
        }

        public void SetTowerType(TowerType type)
        {
            _type = type;
        }

        public void Build()
        {
            if (_selectionService.BuildSite) _buildService.BuildTower(_type, _selectionService.BuildSite, _manager.GetCancellationTokenOnDestroy());
            _selectionService.ClearBuildSiteSelection();
        }

    }
}
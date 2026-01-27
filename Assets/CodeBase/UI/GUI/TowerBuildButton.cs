using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using Infrastructure.Managers;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace Gameplay.UI
{
    public class TowerBuildButton : MonoBehaviour
    {
        public event UnityAction BuildButtonClicked;

        private TowerType _type;

        private ITowerBuildService _towerBuildService;
        private ITowerSelectionService _towerSelectionService;

        [Inject]
        public void Construct(ITowerSelectionService towerSelectionService, ITowerBuildService towerBuildService)
        {
            _towerSelectionService = towerSelectionService;
            _towerBuildService = towerBuildService;
        }

        public void SetTowerType(TowerType type)
        {
            _type = type;
        }

        public void Build()
        {
            BuildButtonClicked?.Invoke();
            if (_towerSelectionService.BuildSite) _towerBuildService.BuildTower(_type, _towerSelectionService.BuildSite);
        }

    }
}
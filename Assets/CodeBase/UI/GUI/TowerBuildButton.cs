using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using Infrastructure.Managers;
using Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class TowerBuildButton : MonoBehaviour
    {
        [SerializeField] private TowerType _type;

        private BuildSite _site;
        private ITowerBuildService _towerBuildService;
        private GameManager _manager;

        [Inject]
        public void Construct(ITowerBuildService towerBuildService, GameManager gameManager)
        {
            _towerBuildService = towerBuildService;
            _manager = gameManager;
        }

        private void OnEnable()
        {
            BuildSite.BuildSiteClicked += SetBuildSite;

        }

        private void OnDisable()
        {
            BuildSite.BuildSiteClicked -= SetBuildSite;
        }

        private void SetBuildSite(BuildSite site)
        {
            _site = site;
        }

        public void Build()
        {
            if (_site) _towerBuildService.BuildTower(_type, _site, _manager.GetCancellationTokenOnDestroy());
        }

    }
}
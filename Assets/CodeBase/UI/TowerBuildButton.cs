using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
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

        [Inject]
        public void Construct(ITowerBuildService towerBuildService)
        {
            _towerBuildService = towerBuildService;
        }

        private void OnEnable()
        {
            Messenger<BuildSite>.AddListener(Events.BuildSiteClicked, SetBuildSite);

        }

        private void OnDisable()
        {
            Messenger<BuildSite>.RemoveListener(Events.BuildSiteClicked, SetBuildSite);
        }

        private void SetBuildSite(BuildSite site)
        {
            _site = site;
        }

        public void Build()
        {
            if (_site) _towerBuildService.BuildTower(_type, _site);
        }

    }
}
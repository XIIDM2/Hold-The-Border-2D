using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using UnityEngine;

namespace Gameplay.UI
{
    public class TowerBuildButton : MonoBehaviour
    {
        [SerializeField] private TowerType _type;

        private BuildSite _site;

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
            if (_site) Messenger<TowerType, BuildSite>.Broadcast(Events.TowerBuildRequested, _type, _site);
        }

    }
}
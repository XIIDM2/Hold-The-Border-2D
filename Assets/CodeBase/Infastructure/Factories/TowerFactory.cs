using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using System.Threading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Factories
{
    public class TowerFactory : ITowerFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly GameplayRegistry _dataCatalog;
        private IObjectResolver _objectResolver;

        public TowerFactory(IAssetProvider assetProvider, GameplayRegistry dataCatalog, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _dataCatalog = dataCatalog;
            _objectResolver = objectResolver;
        }

        public async UniTask<TowerController> CreateTower(TowerType type, Vector2 position)
        {
            TowerData towerData = _dataCatalog.GetTowerData(type);

            if (towerData == null)
            {
                Debug.LogError($"Failed to create tower, {type} data is null");
                return null;
            }

            GameObject towerObject = _objectResolver.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(towerData.TowerPrefabReference), position, Quaternion.identity);
            if (!towerObject.TryGetComponent<TowerController>(out TowerController tower))
            {
                Debug.LogError($"Failed to get TowerController component from tower game object");
                Object.Destroy(towerObject);
                return null;
            }

            tower.Init(towerData);
            return tower;

        }

        public async UniTask<BuildSite> CreateBuildSite(Vector2 position)
        {
            GameObject siteObject = _objectResolver.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(_dataCatalog.BuildSiteReference), position, Quaternion.identity);

            if (!siteObject.TryGetComponent<BuildSite>(out BuildSite site))
            {
                Debug.LogError($"Failed to get Buildsite component from buildsite game object");
                Object.Destroy(siteObject);
                return null;
            }

            return site;
        }
    }
}

using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Factories
{
    public class TowerFactory : ITowerFactory
    {
        private readonly IAssetProviderService _assetProvider;
        private readonly GameplayRegistry _gameplayRegistry;
        private IObjectResolver _objectResolver;

        public TowerFactory(IAssetProviderService assetProvider, GameplayRegistry gameplayRegistry, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _gameplayRegistry = gameplayRegistry;
            _objectResolver = objectResolver;
        }

        public async UniTask<TowerController> CreateTower(TowerType type, Vector2 position)
        {
            TowerData towerData = _gameplayRegistry.GetTowerData(type);

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

            tower.Initialize(towerData);
            return tower;

        }

        public async UniTask<BuildSite> CreateBuildSite(Vector2 position)
        {
            GameObject siteObject = _objectResolver.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(_gameplayRegistry.BuildSiteReference), position, Quaternion.identity);

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

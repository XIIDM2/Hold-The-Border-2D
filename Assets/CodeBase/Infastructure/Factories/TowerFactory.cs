using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using UnityEngine;
using VContainer;

namespace Infrastructure.Factories
{
    public class TowerFactory : BaseFactory, ITowerFactory
    {
        private readonly GameplayRegistry _gameplayRegistry;

        public TowerFactory(IAssetProviderService assetProvider, IObjectResolver objectResolver, GameplayRegistry gameplayRegistry) : base(assetProvider, objectResolver)
        {
            _gameplayRegistry = gameplayRegistry;
        }

        public async UniTask<TowerController> CreateTower(TowerType type, Vector2 position)
        {
            TowerData towerData = _gameplayRegistry.GetTowerData(type);

            if (towerData == null)
            {
                Debug.LogError($"Failed to create tower, {type} data is null");
                return null;
            }

            TowerController tower = await Create<TowerController>(towerData.TowerPrefabReference, position);

            tower.Initialize(towerData);
            return tower;

        }

        public async UniTask<BuildSite> CreateBuildSite(Vector2 position)
        {
            BuildSite site = await Create<BuildSite>(_gameplayRegistry.BuildSiteReference, position);

            return site;
        }
    }
}

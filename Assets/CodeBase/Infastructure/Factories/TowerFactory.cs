using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Towers;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

namespace Infrastructure.Factories
{
    public class TowerFactory : ITowerFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly GameplayRegistry _dataCatalog;

        public TowerFactory(IAssetProvider assetProvider, GameplayRegistry dataCatalog)
        {
            _assetProvider = assetProvider;
            _dataCatalog = dataCatalog;
        }

        public async UniTask CreateTower(TowerType type, Vector2 position)
        {
            TowerData towerData = _dataCatalog.GetTowerData(type);

            if (towerData == null)
            {
                Debug.LogError($"Failed to create tower, {type} data is null");
                return;
            }

            GameObject tower = Object.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(towerData.TowerPrefabReference), position, Quaternion.identity);

            if (tower == null)
            {
                Debug.LogError($"Failed to create tower, {type} prefab is null");
                return;
            }

            tower.GetComponent<TowerController>().Init(towerData);
        }

        public async UniTask CreateBuildSite(Vector2 position)
        {
            GameObject BuildSite = Object.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(_dataCatalog.BuildSiteReference), position, Quaternion.identity);

            if (BuildSite == null)
            {
                Debug.LogError($"Failed to create tower, BuildSite prefab is null");
                return;
            }
        }
    }
}

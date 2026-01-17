using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Towers;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class TowerFactory : ITowerFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DataCatalog _dataCatalog;

        public TowerFactory(IAssetProvider assetProvider, DataCatalog dataCatalog)
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
    }
}

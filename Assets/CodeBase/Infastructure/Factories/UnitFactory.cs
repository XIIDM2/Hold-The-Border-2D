using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Units.Enemy;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class UnitFactory : IUnitFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DataCatalog _dataCatalog;

        public UnitFactory(IAssetProvider assetProvider, DataCatalog dataCatalog)
        {
            _assetProvider = assetProvider;
            _dataCatalog = dataCatalog;
        }

        public async UniTask CreateUnit(EnemyUnitType type, Waypoint start, Vector2 position)
        {
            EnemyUnitData unitData = _dataCatalog.GetUnitData(type);

            if (unitData == null)
            {
                Debug.LogError($"Failed to create unit, {type} data is null");
                return;
            }

            GameObject unit = Object.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(unitData.PrefabReference), position, Quaternion.identity);

            if (unit == null)
            {
                Debug.LogError($"Failed to create unit, {type} prefab is null");
                return;
            }

            unit.GetComponent<EnemyUnitController>().Init(unitData, start);
        }
    }
}
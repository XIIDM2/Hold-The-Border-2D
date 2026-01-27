using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Units.Enemy;
using System.Threading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Factories
{
    public class UnitFactory : IUnitFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly GameplayRegistry _dataCatalog;
        private IObjectResolver _objectResolver;

        public UnitFactory(IAssetProvider assetProvider, GameplayRegistry dataCatalog, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _dataCatalog = dataCatalog;
            _objectResolver = objectResolver;
        }

        public async UniTask<EnemyUnitController> CreateUnit(EnemyUnitType type, Waypoint start, Vector2 position)
        {
            EnemyUnitData unitData = _dataCatalog.GetUnitData(type);

            if (unitData == null)
            {
                Debug.LogError($"Failed to create unit, {type} data is null");
                return null;
            }

            GameObject unit = _objectResolver.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(unitData.PrefabReference), position, Quaternion.identity);

            if (!unit.TryGetComponent<EnemyUnitController>(out EnemyUnitController enemy))
            {
                Debug.LogError($"Failed to get EnemyUnitController component from unit");
                return null;
            }

            enemy.Init(unitData, start);

            return enemy;
        }
    }
}
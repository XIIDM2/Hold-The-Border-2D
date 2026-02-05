using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Path;
using Gameplay.Units.Enemy;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Factories
{
    public class UnitFactory : IUnitFactory
    {
        private readonly IAssetProviderService _assetProvider;
        private readonly GameplayRegistry _gameplayRegistry;
        private IObjectResolver _objectResolver;

        public UnitFactory(IAssetProviderService assetProvider, GameplayRegistry gameplayRegistry, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _gameplayRegistry = gameplayRegistry;
            _objectResolver = objectResolver;
        }

        public async UniTask<EnemyUnitController> CreateUnit(EnemyUnitType type, Waypoint start, Vector2 position)
        {
            EnemyUnitData unitData = _gameplayRegistry.GetUnitData(type);

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

            enemy.Initialize(unitData, start);

            return enemy;
        }
    }
}
using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Path;
using Gameplay.Units.Enemy;
using System.Threading;
using UnityEngine;
using VContainer;

namespace Infrastructure.Factories
{
    public class UnitFactory : BaseFactory, IUnitFactory
    {
        private readonly GameplayRegistry _gameplayRegistry;

        public UnitFactory(IAssetProviderService assetProvider, IObjectResolver objectResolver, GameplayRegistry gameplayRegistry) : base(assetProvider, objectResolver)
        {
            _gameplayRegistry = gameplayRegistry;
        }

        public async UniTask<EnemyUnitController> CreateUnit(EnemyUnitType type, Waypoint start, Vector2 position, CancellationToken cancellationToken)
        {
            EnemyUnitData unitData = _gameplayRegistry.GetUnitData(type);

            if (unitData == null)
            {
                Debug.LogError($"Failed to create unit, {type} data is null");
                return null;
            }

            EnemyUnitController enemy = await Create<EnemyUnitController>(unitData.PrefabReference, position, cancellationToken);

            enemy.Init(unitData, start);

            return enemy;
        }
    }
}
using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Path;
using Gameplay.Units.Enemy;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VContainer;

namespace Infrastructure.Factories
{
    public class UnitFactory : BaseFactory, IUnitFactory
    {
        private readonly GameplayRegistry _gameplayRegistry;
        private readonly Dictionary<EnemyUnitType, Queue<EnemyUnitController>> _pools = new Dictionary<EnemyUnitType, Queue<EnemyUnitController>>();

        private const int MAX_POOL_SIZE = 10;

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

            EnemyUnitController enemy = null;

            if (_pools.TryGetValue(type, out var pool) && pool.Count > 0)
            {
                enemy = pool.Dequeue();
                enemy.transform.position = position;
                enemy.gameObject.SetActive(true);
            }
            else
            {
                enemy = await Create<EnemyUnitController>(unitData.PrefabReference, position, cancellationToken);

                enemy.InitPool(type, this);

            }

            enemy.Init(unitData, start);

            return enemy;
        }

        public async UniTask InitPool(EnemyUnitType type, CancellationToken cancellationToken)
        {
            for (int i = 0; i < MAX_POOL_SIZE; i++)
            {
                EnemyUnitData unitData = _gameplayRegistry.GetUnitData(type);

                EnemyUnitController enemy = await Create<EnemyUnitController>(unitData.PrefabReference, Vector2.zero, cancellationToken);

                enemy.InitPool(type, this);

                ReturnToPool(type, enemy);
            }
        }

        public void ReturnToPool(EnemyUnitType type, EnemyUnitController enemy)
        { 
            enemy.gameObject.SetActive(false);

            if (!_pools.TryGetValue(type, out var pool))
            {
                pool = new Queue<EnemyUnitController>();
                _pools.Add(type, pool);

            }

            if (pool.Count >= MAX_POOL_SIZE)
            {
                Object.Destroy(enemy.gameObject);
            }
            else
            {
                pool.Enqueue(enemy);
            }

        }

    }
}
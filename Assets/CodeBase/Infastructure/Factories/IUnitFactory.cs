using Cysharp.Threading.Tasks;
using Gameplay.Path;
using Gameplay.Units.Enemy;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Factories
{
    public interface IUnitFactory
    {
        event UnityAction<EnemyUnitController> UnitCreated;
        UniTask<EnemyUnitController> CreateUnit(EnemyUnitType type, Waypoint start, Vector2 position, CancellationToken cancellationToken);
        UniTask InitPool(EnemyUnitType type, CancellationToken cancellationToken);
        void ReturnToPool(EnemyUnitType type, EnemyUnitController enemy);
    }
}
using Cysharp.Threading.Tasks;
using Gameplay.Path;
using Gameplay.Units.Enemy;
using System.Threading;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IUnitFactory
    {
        UniTask<EnemyUnitController> CreateUnit(EnemyUnitType type, Waypoint start, Vector2 position, CancellationToken cancellationToken);   
    }
}
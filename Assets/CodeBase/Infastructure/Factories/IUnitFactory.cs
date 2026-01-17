using Cysharp.Threading.Tasks;
using Gameplay.Units.Enemy;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IUnitFactory
    {
        UniTask CreateUnit(EnemyUnitType type, Waypoint start, Vector2 position);
    }
}
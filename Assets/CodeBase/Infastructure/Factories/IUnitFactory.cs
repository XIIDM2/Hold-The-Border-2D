using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IUnitFactory
{
    UniTask<GameObject> CreateUnit(EnemyUnitType type, Waypoint start, Vector2 position);
}

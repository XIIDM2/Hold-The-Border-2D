using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IUnitFactory
{
    UniTask<GameObject> CreateUnit(UnitType type, Waypoint start, Vector2 position);
}

using Data;
using UnityEngine;

namespace Infrastructure.Interfaces
{
    public interface IAttackerRequireable
    {
        void InitAttackers(GameObject unitPrefab, Vector2[] unitsPositions);
    }
}
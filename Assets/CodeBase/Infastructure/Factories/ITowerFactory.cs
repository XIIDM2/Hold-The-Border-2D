using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface ITowerFactory
    {
        UniTask CreateTower(TowerType type, Vector2 position);
    }
}
using Gameplay.Towers;
using UnityEngine;

namespace Infrastructure.Services
{
    public interface ITowerBuildService
    {
        public void BuildTower(TowerType type, Vector2 position);

    }
}
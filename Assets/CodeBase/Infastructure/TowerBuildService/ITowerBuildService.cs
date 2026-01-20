using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using UnityEngine;

namespace Infrastructure.Services
{
    public interface ITowerBuildService
    {
        void BuildTower(TowerType type, BuildSite position);
        void UpgradeTower(TowerController tower);
        void SellTower(TowerController tower);

    }
}
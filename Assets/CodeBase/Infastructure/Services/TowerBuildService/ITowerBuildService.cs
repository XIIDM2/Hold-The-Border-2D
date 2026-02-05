using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;

namespace Infrastructure.Services
{
    public interface ITowerBuildService
    {
        UniTaskVoid BuildTower(TowerType type, BuildSite position);
        void UpgradeTower(TowerController tower);
        UniTaskVoid SellTower(TowerController tower);

    }
}
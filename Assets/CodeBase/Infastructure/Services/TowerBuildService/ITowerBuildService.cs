using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using System.Threading;

namespace Infrastructure.Services
{
    public interface ITowerBuildService
    {
        UniTaskVoid BuildTower(TowerType type, BuildSite position, CancellationToken cancellationToken);
        void UpgradeTower(TowerController tower);
        UniTaskVoid SellTower(TowerController tower, CancellationToken cancellationToken);

    }
}
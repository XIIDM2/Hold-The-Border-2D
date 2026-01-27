using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using System.Threading;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface ITowerFactory
    {
        UniTask<TowerController> CreateTower(TowerType type, Vector2 position, CancellationToken cancellationToken);
        UniTask<BuildSite> CreateBuildSite(Vector2 position, CancellationToken cancellationToken);
    }
}
using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface ITowerFactory
    {
        UniTask<TowerController> CreateTower(TowerType type, Vector2 position);
        UniTask<BuildSite> CreateBuildSite(Vector2 position);
    }
}
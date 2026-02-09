using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Gameplay.UI;
using System.Threading;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IUIFactory
    {
        UniTask<DamagePopup> CreateDamagePopup(Vector2 position, int damage, CancellationToken cancellationToken);
        UniTask<TowerBuildingStatsView> CreateTowerPanel(TowerType type, Sprite icon, string name, string description, string damage, string attackCooldown, string attackRadius, string price, CancellationToken cancellationToken);
    }
}
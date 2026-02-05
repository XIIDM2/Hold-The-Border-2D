using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Gameplay.UI;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IUIFactory
    {
        UniTask<DamagePopup> CreateDamagePopup(Vector2 position, int damage);
        UniTask<TowerPanelUI> CreateTowerPanel(TowerType type, Sprite icon, string name, string description, string damage, string attackCooldown, string attackRadius, string price);
    }
}
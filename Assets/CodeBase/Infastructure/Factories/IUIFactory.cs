using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public interface IUIFactory
{
    UniTask<DamagePopup> CreateDamagePopup(Vector2 position, int damage);
    UniTask<GameObject> CreateTowerPanel(TowerType type, Sprite icon, string name, string description, string damage, string attackCooldown, string attackRadius, string price);
}

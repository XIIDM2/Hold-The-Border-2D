using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Towers;
using Gameplay.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProviderService _assetProvider;
        private readonly GameplayRegistry _gameplayRegistry;
        private IObjectResolver _objectResolver;

        public UIFactory(IAssetProviderService assetProvider, IObjectResolver objectResolver, GameplayRegistry gameplayRegistry)
        {
            _assetProvider = assetProvider;
            _objectResolver = objectResolver;
            _gameplayRegistry = gameplayRegistry;
        }

        public async UniTask<DamagePopup> CreateDamagePopup(Vector2 position, int damage)
        {
            GameObject damagePopupObject = _objectResolver.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(_gameplayRegistry.DamagePopupReference), position, Quaternion.identity);

            if (!damagePopupObject.TryGetComponent<DamagePopup>(out DamagePopup popup))
            {
                Debug.LogError($"Failed to get DamagePopup component from damagePopup game object");
                Object.Destroy(damagePopupObject);
                return null;
            }

            popup.Initialize(damage);

            return popup;
        }

        public async UniTask<GameObject> CreateTowerPanel(TowerType type, Sprite icon, string name, string description, string damage, string attackCooldown, string attackRadius, string price)
        {
            GameObject TowerPanelUIObject = _objectResolver.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(_gameplayRegistry.TowerPanelReference));

            if (!TowerPanelUIObject.TryGetComponent<TowerPanelUI>(out TowerPanelUI towerPanel))
            {
                Debug.LogError($"Failed to get TowerPanelUI component from towerPanel game object");
                Object.Destroy(TowerPanelUIObject);
                return null;
            }

            towerPanel.Initialize(type, icon, name, description, damage, attackCooldown, attackRadius, price);

            return TowerPanelUIObject;
        }
    }
}
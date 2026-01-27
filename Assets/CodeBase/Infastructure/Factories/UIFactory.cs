using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly GameplayRegistry _dataCatalog;
        private IObjectResolver _objectResolver;

        public UIFactory(IAssetProvider assetProvider, IObjectResolver objectResolver, GameplayRegistry dataCatalog)
        {
            _assetProvider = assetProvider;
            _objectResolver = objectResolver;
            _dataCatalog = dataCatalog;
        }

        public async UniTask<DamagePopup> CreateDamagePopup(Vector2 position, int damage)
        {
            GameObject damagePopupObject = _objectResolver.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(_dataCatalog.DamagePopupReference), position, Quaternion.identity);

            if (!damagePopupObject.TryGetComponent<DamagePopup>(out DamagePopup popup))
            {
                Debug.LogError($"Failed to get DamagePopup component from damagePopup game object");
                Object.Destroy(damagePopupObject);
                return null;
            }

            popup.Init(damage);

            return popup;
        }

        public async UniTask<GameObject> CreateTowerPanel(TowerType type, Sprite icon, string name, string description, string damage, string attackCooldown, string attackRadius, string price)
        {
            GameObject TowerPanelUIObject = _objectResolver.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(_dataCatalog.TowerPanelReference));

            if (!TowerPanelUIObject.TryGetComponent<TowerPanelUI>(out TowerPanelUI towerPanel))
            {
                Debug.LogError($"Failed to get TowerPanelUI component from towerPanel game object");
                Object.Destroy(TowerPanelUIObject);
                return null;
            }

            towerPanel.Init(type, icon, name, description, damage, attackCooldown, attackRadius, price);

            return TowerPanelUIObject;
        }
    }
}
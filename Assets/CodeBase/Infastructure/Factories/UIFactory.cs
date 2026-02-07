using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Towers;
using Gameplay.UI;
using System.Threading;
using UnityEngine;
using VContainer;

namespace Infrastructure.Factories
{
    public class UIFactory : BaseFactory, IUIFactory
    {
        private readonly GameplayRegistry _gameplayRegistry;

        public UIFactory(IAssetProviderService assetProvider, IObjectResolver objectResolver, GameplayRegistry gameplayRegistry) : base(assetProvider, objectResolver)
        {
            _gameplayRegistry = gameplayRegistry;
        }

        public async UniTask<DamagePopup> CreateDamagePopup(Vector2 position, int damage, CancellationToken cancellationToken)
        {
            DamagePopup popup = await Create<DamagePopup>(_gameplayRegistry.DamagePopupReference, position, cancellationToken);

            popup.Initialize(damage);

            return popup;
        }

        public async UniTask<TowerPanelUI> CreateTowerPanel(TowerType type, Sprite icon, string name, string description, string damage, string attackCooldown, string attackRadius, string price, CancellationToken cancellationToken)
        {
            TowerPanelUI towerPanel = await Create<TowerPanelUI>(_gameplayRegistry.TowerPanelReference, Vector2.zero, cancellationToken);

            towerPanel.Initialize(type, icon, name, description, damage, attackCooldown, attackRadius, price);

            return towerPanel;
        }
    }
}
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
        private readonly UIRegistry _UIRegistry;

        public UIFactory(IAssetProviderService assetProvider, IObjectResolver objectResolver, UIRegistry UIRegistry) : base(assetProvider, objectResolver)
        {
            _UIRegistry = UIRegistry;
        }

        public async UniTask<LevelButton>CreateLevelButton(string levelName, string addressablesLabel, CancellationToken cancellationToken)
        {
            LevelButton levelButton = await Create<LevelButton>(_UIRegistry.LevelButtonReference, Vector2.zero, cancellationToken);

            levelButton.Init(levelName, addressablesLabel);

            return levelButton;
        }

        public async UniTask<DamagePopup>CreateDamagePopup(Vector2 position, int damage, CancellationToken cancellationToken)
        {
            DamagePopup popup = await Create<DamagePopup>(_UIRegistry.DamagePopupReference, position, cancellationToken);

            popup.Init(damage);

            return popup;
        }

        public async UniTask<TowerBuildingStatsView> CreateTowerPanel(TowerType type, Sprite icon, string name, string description, string damage, string attackCooldown, string attackRadius, string price, CancellationToken cancellationToken)
        {
            TowerBuildingStatsView towerPanel = await Create<TowerBuildingStatsView>(_UIRegistry.TowerPanelReference, Vector2.zero, cancellationToken);

            towerPanel.Init(type, icon, name, description, damage, attackCooldown, attackRadius, price);

            return towerPanel;
        }
    }
}
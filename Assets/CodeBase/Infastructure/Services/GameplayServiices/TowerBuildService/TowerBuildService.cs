using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Player;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using Infrastructure.Events;
using Infrastructure.Factories;
using System.Threading;
using UnityEngine;

namespace Infrastructure.Services
{
    public class TowerBuildService : ITowerBuildService
    {
        private IPlayerController _player;
        private ITowerFactory _factory;

        private IEventBus _eventBus;

        private GameplayRegistry _gameplayRegistry;
        private SFXRegistry _SFXRegistry;

        public TowerBuildService(IPlayerController player, ITowerFactory factory, IEventBus eventBus, GameplayRegistry gameplayRegistry, SFXRegistry SFXRegistry)
        {
            _player = player;
            _factory = factory;
            _eventBus = eventBus;
            _gameplayRegistry = gameplayRegistry;
            _SFXRegistry = SFXRegistry;
        }


        public async UniTask BuildTower(TowerType type, BuildSite site, CancellationToken cancellationToken)
        {
            int buildPrice = _gameplayRegistry.GetTowerData(type).BuildPrice;

            if (buildPrice > _player.Gold)
            {
                return;
            }

            TowerController tower = await _factory.CreateTower(type, site.transform.position, cancellationToken);

            if (!tower)
            {
                Debug.LogError($"Failed to build {tower}");
                return;
            }

            Object.Destroy(site.gameObject);
            _player.TrySpendGold(buildPrice);
            _eventBus.Publish(new InvokeSFX(_SFXRegistry.CoinsSound));

        }

        public void UpgradeTower(TowerController tower)
        {
            int upgradePrice = tower.CurrentTierConfig.UpgradePrice;

            if (upgradePrice > _player.Gold)
            {
                return;
            }

            _player.TrySpendGold(upgradePrice);
            tower.UpgradeRequested?.Invoke();
            _eventBus.Publish(new InvokeSFX(_SFXRegistry.CoinsSound));
        }

        public async UniTask SellTower(TowerController tower, CancellationToken cancellationToken)
        {
            int sellPrice = tower.CurrentTierConfig.SellPrice;
            Vector2 position = tower.transform.position;

            BuildSite buildSite = await _factory.CreateBuildSite(position, cancellationToken);

            if (!buildSite)
            {
                Debug.LogError($"Failed to sell {tower}");
                return;
            }

            _player.AddGold(sellPrice);
            Object.Destroy(tower.gameObject);
            _eventBus.Publish(new InvokeSFX(_SFXRegistry.CoinsSound));

        }
    }
}
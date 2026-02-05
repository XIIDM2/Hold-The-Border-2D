using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Player;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using Infrastructure.Factories;
using UnityEngine;

namespace Infrastructure.Services
{
    public class TowerBuildService : ITowerBuildService
    {
        private IPlayerController _player;
        private GameplayRegistry _gameplayRegistry;

        private ITowerFactory _factory;

        public TowerBuildService(IPlayerController player, GameplayRegistry gameplayRegistry, ITowerFactory factory)
        {
            _player = player;
            _gameplayRegistry = gameplayRegistry;
            _factory = factory;
        }


        public async UniTaskVoid BuildTower(TowerType type, BuildSite site)
        {
            int buildPrice = _gameplayRegistry.GetTowerData(type).BuildPrice;

            if (buildPrice > _player.Gold)
            {
                Debug.Log("Not enough gold");
                return;
            }

            TowerController tower = await _factory.CreateTower(type, site.transform.position);

            if (!tower)
            {
                Debug.LogError("Failed to build tower");
                return;
            }

            Object.Destroy(site.gameObject);
            _player.TrySpendGold(buildPrice);

        }

        public void UpgradeTower(TowerController tower)
        {
            int upgradePrice = tower.CurrentTierConfig.UpgradePrice;

            if (upgradePrice > _player.Gold)
            {
                Debug.Log("Not enough gold");
                return;
            }

            _player.TrySpendGold(upgradePrice);
            tower.UpgradeRequested?.Invoke();
        }

        public async UniTaskVoid SellTower(TowerController tower)
        {
            int sellPrice = tower.CurrentTierConfig.SellPrice;
            Vector2 position = tower.transform.position;

            BuildSite buildSite = await _factory.CreateBuildSite(position);

            if (!buildSite)
            {
                Debug.LogError("Failed to sell tower");
                return;
            }

            _player.AddGold(sellPrice);
            Object.Destroy(tower.gameObject);

        }
    }
}
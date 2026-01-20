using Data;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using Infrastructure.Factories;
using System;
using System.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Infrastructure.Services
{
    public class TowerBuildService : ITowerBuildService, IInitializable, IDisposable
    {
        private IPlayerController _player;
        private GameplayRegistry _catalog;

        private ITowerFactory _factory;

        public TowerBuildService(IPlayerController player, GameplayRegistry catalog, ITowerFactory factory)
        {
            _player = player;
            _catalog = catalog;
            _factory = factory;
        }

        public void Initialize()
        {
            Messenger<TowerType, BuildSite>.AddListener(Events.TowerBuildRequested, BuildTower);
            Messenger<TowerController>.AddListener(Events.TowerUpgradeRequested, UpgradeTower); 
            Messenger<TowerController>.AddListener(Events.TowerSellRequested, SellTower);
        }

        public void Dispose()
        {
            Messenger<TowerType, BuildSite>.RemoveListener(Events.TowerBuildRequested, BuildTower);
            Messenger<TowerController>.RemoveListener(Events.TowerUpgradeRequested, UpgradeTower);
            Messenger<TowerController>.RemoveListener(Events.TowerSellRequested, SellTower);
        }

        public async void BuildTower(TowerType type, BuildSite site)
        {
            int buildPrice = _catalog.GetTowerData(type).BuildPrice;

            if (buildPrice > _player.Gold)
            {
                Debug.Log("Not enough gold");
                return;
            }


            UnityEngine.Object.Destroy(site.gameObject);
            _player.TrySpendGold(buildPrice);
            await _factory.CreateTower(type, site.transform.position);

        }

        public void UpgradeTower(TowerController tower)
        {
            int upgradePrice = tower.currentTierConfig.UpgradePrice;

            if (upgradePrice > _player.Gold)
            {
                Debug.Log("Not enough gold");
                return;
            }
            _player.TrySpendGold(upgradePrice);
            tower.UpgradeRequested?.Invoke();
        }

        public async void SellTower(TowerController tower)
        {
            int sellPrice = tower.currentTierConfig.SellPrice;
            Vector2 position = tower.transform.position;

            _player.GetGold(sellPrice);

            UnityEngine.Object.Destroy(tower.gameObject);
            await _factory.CreateBuildSite(position);
        }
    }
}
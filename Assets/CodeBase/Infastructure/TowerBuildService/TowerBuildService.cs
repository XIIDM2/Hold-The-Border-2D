using Data;
using Gameplay.Towers;
using Infrastructure.Factories;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Services
{
    public class TowerBuildService : ITowerBuildService, IInitializable, IDisposable
    {
        private IPlayerController _player;
        private DataCatalog _catalog;

        private ITowerFactory _factory;

        public TowerBuildService(IPlayerController player, DataCatalog catalog, ITowerFactory factory)
        {
            _player = player;
            _catalog = catalog;
            _factory = factory;
        }

        public void Initialize()
        {
            Messenger<TowerType, Vector2>.AddListener(Events.TowerBuildRequested, BuildTower);
            Messenger<TowerController>.AddListener(Events.TowerUpgradeRequested, UpdateTower);
        }

        public void Dispose()
        {
            Messenger<TowerType, Vector2>.RemoveListener(Events.TowerBuildRequested, BuildTower);
            Messenger<TowerController>.RemoveListener(Events.TowerUpgradeRequested, UpdateTower);
        }

        public async void BuildTower(TowerType type, Vector2 position)
        {
            int price = _catalog.GetTowerData(type).BuildPrice;

            if (price >= _player.Gold)
            {
                Debug.Log("Not enough gold");
                return;
            }


            await _factory.CreateTower(type, position);

        }

        public void UpdateTower(TowerController tower)
        {
            int price = tower.currentTierConfig.UpgradePrice;

            if (price >= _player.Gold)
            {
                Debug.Log("Not enough gold");
                return;
            }

            tower.UpgradeRequested?.Invoke();
        }


        // upgradetower
    }
}
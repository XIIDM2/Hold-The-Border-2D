using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Infrastructure.Managers;
using Infrastructure.Services;
using System;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class TowerPresenter : IStartable, IDisposable
    {
        private TowerController _tower;

        private TowerView _view;

        private ITowerSelectionService _selectionService;
        private ITowerBuildService _buildService;

        private LevelManager _manager;

        public TowerPresenter(TowerView view, ITowerSelectionService selectionService, ITowerBuildService buildService, LevelManager manager)
        {
            _view = view;
            _selectionService = selectionService;
            _buildService = buildService;
            _manager = manager;
        }

        public void Start()
        {
            _selectionService.TowerSelected += ShowTowerView;
            _selectionService.TowerDeselected += _view.HideTowerPanel;
            _view.UpgradeButtonHowevered += ShowUpgradePanel;

            _view.UpgradeRequested += Upgrade;
            _view.SellRequested += Sell;
        }

        public void Dispose()
        {
            _selectionService.TowerSelected -= ShowTowerView;
            _selectionService.TowerDeselected -= _view.HideTowerPanel;
            _view.UpgradeButtonHowevered -= ShowUpgradePanel;

            _view.UpgradeRequested -= Upgrade;
            _view.SellRequested -= Sell;
        }

        private void ShowTowerView(TowerController tower)
        {
            _tower = tower;

            if (tower.CurrentTierIndex >= tower.MaxTier) _view.UpgradeButton.gameObject.SetActive(false);

            _view.ShowTowerPanel(tower.CurrentTierConfig.UpgradePrice.ToString(), tower.CurrentTierConfig.SellPrice.ToString(), tower.transform.position);
        }

        private void ShowUpgradePanel()
        {
            _view.ShowUpgradePanel(
                _tower.CurrentTierConfig.Damage.ToString(), _tower.NextTierConfig.Damage.ToString(),
                _tower.CurrentTierConfig.AttackCooldown.ToString(), _tower.NextTierConfig.AttackCooldown.ToString(),
                _tower.CurrentTierConfig.AttackRadius.ToString(), _tower.NextTierConfig.AttackRadius.ToString());
        }

        private void Upgrade()
        {
            if (_selectionService.Tower)
            {
                _buildService.UpgradeTower(_tower);
                _selectionService.ClearTowerSelection();
            }
        }

        private void Sell()
        {
            if (_selectionService.Tower)
            {
                _buildService.SellTower(_tower, _manager.GetCancellationTokenOnDestroy()).Forget();
                _selectionService.ClearTowerSelection();
            }
        }


    }
}
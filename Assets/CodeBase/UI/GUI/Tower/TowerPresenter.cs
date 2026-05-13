using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Towers;
using Gameplay.Towers.TargetSelectionStrategies;
using Infrastructure.Services;
using System;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class TowerPresenter : IStartable, IDisposable
    {
        private TowerController _tower;

        private TowerView _view;

        private readonly ITowerSelectionService _selectionService;
        private readonly ITowerBuildService _buildService;

        private readonly IPlayerController _player;

        private readonly IRadiusVisualizerService _radiusVisualizerService;
        private readonly CancellationToken _ctc;

        public TowerPresenter(TowerView view, ITowerSelectionService selectionService, ITowerBuildService buildService, IPlayerController player, IRadiusVisualizerService radiusVisualizerService, CancellationToken ctc)
        {
            _view = view;
            _selectionService = selectionService;
            _buildService = buildService;
            _player = player;
            _radiusVisualizerService = radiusVisualizerService;
            _ctc = ctc;
        }

        public void Start()
        {
            _selectionService.TowerSelected += ShowTowerView;
            _selectionService.TowerDeselected += HideTowerView;

            _view.ControllerPointerExit += HideTowerView;

            _view.UpgradeButtonPointerEnter += ShowUpgradePanel;
            _view.UpgradeButtonPointerExit += HideUpgradePanel;

            _view.UpgradeRequested += OnUpgradeRequested;
            _view.SellRequested += OnSellRequested;

            _view.StrategyChanged += OnStrategyChanged;
        }

        public void Dispose()
        {
            _selectionService.TowerSelected -= ShowTowerView;
            _selectionService.TowerDeselected -= HideTowerView;

            _view.ControllerPointerExit -= HideTowerView;

            _view.UpgradeButtonPointerEnter -= ShowUpgradePanel;
            _view.UpgradeButtonPointerExit -= HideUpgradePanel;

            _view.UpgradeRequested -= OnUpgradeRequested;
            _view.SellRequested -= OnSellRequested;

            _view.StrategyChanged -= OnStrategyChanged;
        }

        private void ShowTowerView(TowerController tower)
        {
            _tower = tower;

            if (tower.CurrentTierIndex >= tower.MaxTier) _view.UpgradeButton.gameObject.SetActive(false);

            _view.ShowTowerPanel(tower.CurrentTierConfig.UpgradePrice.ToString(), tower.CurrentTierConfig.SellPrice.ToString(), tower.transform.position);

            if (_player.Gold >= _tower.CurrentTierConfig.UpgradePrice) _view.ShowUpgradeButton();
            else _view.HideUpgradeButton();

            _radiusVisualizerService.ShowVisualizer(_tower, _tower.CurrentTierConfig.AttackRadius);
        }

        private void HideTowerView()
        {
            _view.HideTowerPanel();
            _radiusVisualizerService.HideVisualizer();
        }

        private void ShowUpgradePanel()
        {
            _view.ShowUpgradePanel(
                _tower.CurrentTierConfig.Damage.ToString(), _tower.NextTierConfig.Damage.ToString(),
                _tower.CurrentTierConfig.AttackCooldown.ToString(), _tower.NextTierConfig.AttackCooldown.ToString(),
                _tower.CurrentTierConfig.AttackRadius.ToString(), _tower.NextTierConfig.AttackRadius.ToString());

            _radiusVisualizerService.ShowVisualizer(_tower, _tower.NextTierConfig.AttackRadius);
        }

        private void HideUpgradePanel()
        {
            _view.HideUpgradePanel();

            _radiusVisualizerService.ShowVisualizer(_tower, _tower.CurrentTierConfig.AttackRadius);
        }

        private void OnUpgradeRequested()
        {
            if (_selectionService.Tower)
            {
                _buildService.UpgradeTower(_tower);
                _selectionService.ClearTowerSelection();
            }

            _view.HideUpgradePanel();
            _radiusVisualizerService.HideVisualizer();
        }

        private void OnSellRequested()
        {
            if (_selectionService.Tower)
            {
                _buildService.SellTower(_tower, _ctc).Forget();
                _selectionService.ClearTowerSelection();
            }
        }

        private void OnStrategyChanged(int strategyIndex)
        {
            if (_selectionService.Tower) _tower.Attack.SelectStrategy(strategyIndex);
        }

    }
}
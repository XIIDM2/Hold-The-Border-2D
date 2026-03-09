using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Gameplay.Towers.TargetSelectionStrategies;
using Infrastructure.Managers;
using Infrastructure.Services;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class TowerPresenter : IStartable, IDisposable
    {
        private const int CLOSEST_TO_TOWER_STRATEGY_INDEX = 1;
        private const int CLOSEST_TO_BASE_STRATEGY_INDEX = 2;
        private const int LOWEST_HEALTH_STRATEGY_INDEX = 3;

        private TowerController _tower;
        private Transform _baseTransform;

        private TowerView _view;

        private ITowerSelectionService _selectionService;
        private ITowerBuildService _buildService;

        private IRadiusVisualizerService _radiusVisualizerService;
        private ILevelManager _manager;

        public TowerPresenter(Transform baseTransform, TowerView view, ITowerSelectionService selectionService, ITowerBuildService buildService, IRadiusVisualizerService radiusVisualizerService, ILevelManager manager)
        {
            _baseTransform = baseTransform;
            _view = view;
            _selectionService = selectionService;
            _buildService = buildService;
            _radiusVisualizerService = radiusVisualizerService;
            _manager = manager;
        }

        public void Start()
        {
            _selectionService.TowerSelected += ShowTowerView;
            _selectionService.TowerDeselected += HideTowerView;

            _view.ControllerPointerExit += HideTowerView;

            _view.UpgradeButtonPointerEnter += ShowUpgradePanel;
            _view.UpgradeButtonPointerExit += HideUpgradePanel;

            _view.UpgradeRequested += Upgrade;
            _view.SellRequested += Sell;

            _view.StrategyChanged += OnStrategyChanged;
        }

        public void Dispose()
        {
            _selectionService.TowerSelected -= ShowTowerView;
            _selectionService.TowerDeselected -= HideTowerView;

            _view.ControllerPointerExit -= HideTowerView;

            _view.UpgradeButtonPointerEnter -= ShowUpgradePanel;
            _view.UpgradeButtonPointerExit -= HideUpgradePanel;

            _view.UpgradeRequested -= Upgrade;
            _view.SellRequested -= Sell;

            _view.StrategyChanged -= OnStrategyChanged;
        }

        private void ShowTowerView(TowerController tower)
        {
            _tower = tower;

            if (tower.CurrentTierIndex >= tower.MaxTier) _view.UpgradeButton.gameObject.SetActive(false);

            _view.ShowTowerPanel(tower.CurrentTierConfig.UpgradePrice.ToString(), tower.CurrentTierConfig.SellPrice.ToString(), tower.transform.position);

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

        private void Upgrade()
        {
            if (_selectionService.Tower)
            {
                _buildService.UpgradeTower(_tower);
                _selectionService.ClearTowerSelection();
                _radiusVisualizerService.HideVisualizer();
            }
        }

        private void Sell()
        {
            if (_selectionService.Tower)
            {
                _buildService.SellTower(_tower, _manager.GameObject.GetCancellationTokenOnDestroy()).Forget();
                _selectionService.ClearTowerSelection();
            }
        }

        private void OnStrategyChanged(int strategyIndex)
        {
            if (_selectionService.Tower)
            {
                switch (strategyIndex)
                {
                    case CLOSEST_TO_TOWER_STRATEGY_INDEX:
                        _tower.Attack.SelectStrategy(new ClosestToTowerStrategy(_tower.transform.position));
                        break;
                    case CLOSEST_TO_BASE_STRATEGY_INDEX:
                        _tower.Attack.SelectStrategy(new ClosestToBaseStrategy());
                        break;
                    case LOWEST_HEALTH_STRATEGY_INDEX:
                        _tower.Attack.SelectStrategy(new LowestHealthStrategy());
                        break;
                }
            }
        }


    }
}
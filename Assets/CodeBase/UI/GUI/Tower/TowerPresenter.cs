using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Towers;
using Infrastructure.Services;
using System;
using System.Threading;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class TowerPresenter : IStartable, IDisposable
    {
        private TowerView _view;

        private readonly ITowerSelectionService _selectionService;
        private readonly ITowerBuildService _buildService;

        private readonly IPlayerController _player;

        private readonly IVisualizerService _radiusVisualizerService;
        private readonly CancellationToken _ctc;

        public TowerPresenter(TowerView view, ITowerSelectionService selectionService, ITowerBuildService buildService, IPlayerController player, IVisualizerService radiusVisualizerService, CancellationToken ctc)
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

            _player.GoldChanged += OnGoldChanged;

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

            _player.GoldChanged -= OnGoldChanged;

            _view.ControllerPointerExit -= HideTowerView;

            _view.UpgradeButtonPointerEnter -= ShowUpgradePanel;
            _view.UpgradeButtonPointerExit -= HideUpgradePanel;

            _view.UpgradeRequested -= OnUpgradeRequested;
            _view.SellRequested -= OnSellRequested;

            _view.StrategyChanged -= OnStrategyChanged;
        }

        private void ShowTowerView(TowerController tower)
        {
            if (tower.CurrentTierIndex >= tower.MaxTier) _view.UpgradeButton.gameObject.SetActive(false);

            _view.ShowTowerPanel(tower.CurrentTierConfig.UpgradePrice.ToString(), tower.CurrentTierConfig.SellPrice.ToString(), tower.transform.position);

            OnGoldChanged(_player.Gold);

            _radiusVisualizerService.ShowVisualizer(tower.transform.position, _selectionService.Tower.CurrentTierConfig.AttackRadius);
        }

        private void HideTowerView()
        {
            _view.HideTowerPanel();
            _radiusVisualizerService.HideVisualizer();
        }

        private void ShowUpgradePanel()
        {
            _view.ShowUpgradePanel(
                _selectionService.Tower.CurrentTierConfig.Damage.ToString(), _selectionService.Tower.NextTierConfig.Damage.ToString(),
                _selectionService.Tower.CurrentTierConfig.AttackCooldown.ToString(), _selectionService.Tower.NextTierConfig.AttackCooldown.ToString(),
                _selectionService.Tower.CurrentTierConfig.AttackRadius.ToString(), _selectionService.Tower.NextTierConfig.AttackRadius.ToString());

            _radiusVisualizerService.ShowVisualizer(_selectionService.Tower.transform.position, _selectionService.Tower.NextTierConfig.AttackRadius);
        }

        private void HideUpgradePanel()
        {
            _view.HideUpgradePanel();

            _radiusVisualizerService.ShowVisualizer(_selectionService.Tower.transform.position, _selectionService.Tower.CurrentTierConfig.AttackRadius);
        }

        private void OnGoldChanged(int currentGold)
        {
            if (!_selectionService.Tower) return;

            bool canAfford = currentGold >= _selectionService.Tower.CurrentTierConfig.UpgradePrice;

            _view.SetInteractableUpgradeButton(canAfford);
        }

        private void OnUpgradeRequested()
        {
            if (_selectionService.Tower)
            {
                _buildService.UpgradeTower(_selectionService.Tower);
                _selectionService.ClearTowerSelection();
            }

            _view.HideUpgradePanel();
            _radiusVisualizerService.HideVisualizer();
        }

        private void OnSellRequested()
        {
            if (_selectionService.Tower)
            {
                _buildService.SellTower(_selectionService.Tower, _ctc).Forget();
                _selectionService.ClearTowerSelection();
            }
        }

        private void OnStrategyChanged(int strategyIndex)
        {
            if (_selectionService.Tower) _selectionService.Tower.Attack.SelectStrategy(strategyIndex);
        }

    }
}
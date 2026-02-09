using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using Infrastructure;
using Infrastructure.Factories;
using Infrastructure.Managers;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class TowerBuildingPresenter : IAsyncStartable, IDisposable
    {
        private List<TowerBuildingStatsView> _towerStatsViews = new List<TowerBuildingStatsView>();
        private readonly TowerBuildingView _view;

        private readonly IUIFactory _UIFactory;
        private readonly ITowerSelectionService _selectionService;
        private readonly ITowerBuildService _buildService;

        private readonly SceneController _controller;
        private readonly GameplayRegistry _registry;
        private readonly LevelManager _manager;

        public TowerBuildingPresenter(TowerBuildingView view, IUIFactory uIFactory, ITowerSelectionService selectionService, ITowerBuildService buildService, SceneController controller, GameplayRegistry registry, LevelManager manager)
        {
            _view = view;
            _UIFactory = uIFactory;
            _selectionService = selectionService;
            _buildService = buildService;
            _controller = controller;
            _registry = registry;
            _manager = manager;
        }

        public async UniTask StartAsync(CancellationToken cancellation = default)
        {
            foreach (TowerData towerData in _registry.TowerDatas)
            {
                TowerBuildingStatsView towerPanelView = await _UIFactory.CreateTowerPanel
                (
                    towerData.Type, towerData.Icon, towerData.Name, towerData.Description,
                    towerData.TierConfigs[0].Damage.ToString(),
                    towerData.TierConfigs[0].AttackCooldown.ToString(),
                    towerData.TierConfigs[0].AttackRadius.ToString(),
                    towerData.BuildPrice.ToString(),
                    cancellation
                );

                towerPanelView.gameObject.transform.SetParent(_view.BuildingPanel.transform);

                towerPanelView.BuildRequested += Build;

                _towerStatsViews.Add(towerPanelView);
            }



            _selectionService.BuildsiteSelected += ShowBuildingPanel;
            _selectionService.BuildSiteDeselected += HideBuildingPanel;

            _view.ClosePanelButton.onClick.AddListener(HideBuildingPanel);

        }

        public void Dispose()
        {
            _selectionService.BuildsiteSelected -= ShowBuildingPanel;
            _selectionService.BuildSiteDeselected -= HideBuildingPanel;

            _view.ClosePanelButton.onClick.RemoveListener(HideBuildingPanel);

            foreach (TowerBuildingStatsView towerPanelView in _towerStatsViews)
            {
                towerPanelView.BuildRequested -= Build;
            }
        }

        private void ShowBuildingPanel(BuildSite site)
        {
            _view.ShowBuildingPanel();
            _controller.StopTime();
        }

        private void HideBuildingPanel()
        {
            _view.HideBuildingPanel();
            _controller.StartTime();
        }

        private void Build(TowerType type)
        {
            if (_selectionService.BuildSite) _buildService.BuildTower(type, _selectionService.BuildSite, _manager.GetCancellationTokenOnDestroy()).Forget();
            _selectionService.ClearBuildSiteSelection();
        }
    }
}
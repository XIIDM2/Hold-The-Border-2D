using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Towers;
using Gameplay.Towers.BuildSite;
using Infrastructure;
using Infrastructure.Events;
using Infrastructure.Factories;
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

        private readonly IEventBus _eventBus;

        private readonly SceneController _controller;
        private readonly GameplayRegistry _registry;
        private readonly CancellationToken _ctc;

        public TowerBuildingPresenter(TowerBuildingView view, IUIFactory uIFactory, ITowerSelectionService selectionService, ITowerBuildService buildService, IEventBus eventBus, SceneController controller, GameplayRegistry registry, CancellationToken ctc)
        {
            _view = view;
            _UIFactory = uIFactory;
            _selectionService = selectionService;
            _buildService = buildService;
            _eventBus = eventBus;
            _controller = controller;
            _registry = registry;
            _ctc = ctc;
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

                towerPanelView.BuildRequested += BuildRequested;

                _towerStatsViews.Add(towerPanelView);
            }

            _selectionService.BuildsiteSelected += ShowBuildingPanel;
            _selectionService.BuildSiteDeselected += HideBuildingPanel;

            _view.PanelCloseRequested += HideBuildingPanel;

        }

        public void Dispose()
        {
            _selectionService.BuildsiteSelected -= ShowBuildingPanel;
            _selectionService.BuildSiteDeselected -= HideBuildingPanel;

            _view.PanelCloseRequested -= HideBuildingPanel;

            foreach (TowerBuildingStatsView towerPanelView in _towerStatsViews)
            {
                towerPanelView.BuildRequested -= BuildRequested;
            }
        }

        private void ShowBuildingPanel(BuildSite site)
        {
            _eventBus.Publish(new UIStateChanged(UIStates.InTowerBuildingPanel));
            _view.ShowBuildingPanel();
            _controller.StopTime();
        }

        private void HideBuildingPanel()
        {
            _eventBus.Publish(new UIStateChanged(UIStates.InActiveGameplay));
            _view.HideBuildingPanel();
            _controller.StartTime();
        }

        private void BuildRequested(TowerType type)
        {
            if (_selectionService.BuildSite) _buildService.BuildTower(type, _selectionService.BuildSite, _ctc).Forget();
            _selectionService.ClearBuildSiteSelection();
        }
    }
}
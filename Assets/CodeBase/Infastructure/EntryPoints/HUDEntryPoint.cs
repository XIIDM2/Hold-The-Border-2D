// Main purpose of this script is to make all UI Scripts simple, stupid and make Single entry point of all initialization of UI GameObjects
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Player;
using Gameplay.Towers;
using Gameplay.UI;
using Infrastructure;
using Infrastructure.Factories;
using Infrastructure.Managers;
using Infrastructure.Services;
using System;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

public class HUDEntryPoint : IAsyncStartable, IDisposable
{
    [Header("UI GameObjects")]
    private readonly PlayerStatsUI _playerStats;
    private readonly WavesUI _waves;
    private readonly PauseUI _pause;
    private readonly TowerBuildingControllerUI _buildingController;
    private readonly TowerControllerUI _towerController;

    [Header("Dependencies")]
    private readonly IPlayerController _player;
    private readonly IWaveControllerService _waveControllerService;

    private readonly ITowerSelectionService _selectionService;
    private readonly ITowerBuildService _buildService;

    private readonly IUIFactory _UIFactory;

    private readonly LevelEntryPoint _manager;

    private readonly SceneController _sceneController;
    private readonly GameplayRegistry _registry;

    public HUDEntryPoint(
        PlayerStatsUI playerStats,
        WavesUI waves, PauseUI pause,
        TowerBuildingControllerUI towerBuildingControllerUI,
        TowerControllerUI towerControllerUI,
        IPlayerController player, IWaveControllerService waveControllerService, 
        ITowerSelectionService selectionService, ITowerBuildService buildService, 
        IUIFactory UIFactory, LevelEntryPoint manager, 
        SceneController sceneController, GameplayRegistry registry)
    {
        _playerStats = playerStats;
        _waves = waves;
        _pause = pause;
        _buildingController = towerBuildingControllerUI;
        _towerController = towerControllerUI;

        _player = player;
        _waveControllerService = waveControllerService;

        _selectionService = selectionService;
        _buildService = buildService;

        _UIFactory = UIFactory;

        _manager = manager;

        _sceneController = sceneController;
        _registry = registry;

    }

    //Initialization of all UI GameObjects
    public async UniTask StartAsync(CancellationToken cancellation = default)
    {
        _playerStats.Init(_player.Health.CurrentHealth, _player.Gold);

        _waves.Init(_waveControllerService.CurrentWaveIndex, _waveControllerService.WavesLength);

        _pause.Init(_sceneController);

        _buildingController.Init(_sceneController);

        foreach (TowerData towerData in _registry.TowerDatas)
        {
            await _buildingController.CreateTowerPanels(towerData, _UIFactory, cancellation);
        }

        _towerController.Init();

        SubscribeEvents();
    }


    public void Dispose()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _player.Health.HealthChanged += _playerStats.OnHealthChanged;

        _player.GoldChanged += _playerStats.OnGoldChanged;

        _waveControllerService.NextWaveStarted += _waves.OnNextWaveStarted;

        _selectionService.BuildsiteSelected += _buildingController.ShowBuildingPanel;
        _selectionService.BuildSiteDeselected += _buildingController.HideBuildingPanel;

        _selectionService.TowerSelected += _towerController.ShowController;
        _selectionService.TowerDeselected += _towerController.HideController;
        _selectionService.TowerDeselected += _towerController.HideUpgradePanel;

        _towerController.UpgradeRequested += _buildService.UpgradeTower;
        _towerController.SellRequested += OnTowerSellRequested;
    }

    private void UnSubscribeEvents()
    {
        _player.Health.HealthChanged -= _playerStats.OnHealthChanged;
        _player.GoldChanged -= _playerStats.OnGoldChanged;

        _waveControllerService.NextWaveStarted -= _waves.OnNextWaveStarted;

        _selectionService.BuildsiteSelected -= _buildingController.ShowBuildingPanel;
        _selectionService.BuildSiteDeselected -= _buildingController.HideBuildingPanel;

        _selectionService.TowerSelected -= _towerController.ShowController;
        _selectionService.TowerDeselected -= _towerController.HideController;
        _selectionService.TowerDeselected -= _towerController.HideUpgradePanel;

        _towerController.UpgradeRequested -= _buildService.UpgradeTower;
        _towerController.SellRequested -= OnTowerSellRequested;
    }

    // Handle async operation via sync wrapper method
    private void OnTowerSellRequested(TowerController tower) => _buildService.SellTower(tower, _manager.GetCancellationTokenOnDestroy()).Forget();
}

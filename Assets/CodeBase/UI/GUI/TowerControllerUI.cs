using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Infrastructure.Managers;
using Infrastructure.Services;
using UnityEngine;
using VContainer;

public class TowerControllerUI : MonoBehaviour
{
    private TowerController _tower;

    private ITowerBuildService _towerBuildService;
    private GameManager _manager;

    [Inject]
    public void Construct(ITowerBuildService towerBuildService, GameManager gameManager)
    {
        _towerBuildService = towerBuildService;
        _manager = gameManager;
    }

    private void OnEnable()
    {
       TowerController.TowerControllerClicked += SetTower;
    }

    private void OnDisable()
    {
        TowerController.TowerControllerClicked -= SetTower;
    }

    private void SetTower(TowerController tower)
    {
        _tower = tower;
    }

    public void Upgrade()
    {
        if (_tower) _towerBuildService.UpgradeTower(_tower);
    }

    public void Sell()
    {
        if (_tower) _towerBuildService.SellTower(_tower, _manager.GetCancellationTokenOnDestroy());
    }

}

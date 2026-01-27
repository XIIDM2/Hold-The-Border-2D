using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Infrastructure.Managers;
using Infrastructure.Services;
using UnityEngine;
using VContainer;

public class TowerControllerUI : MonoBehaviour
{

    private ITowerSelectionService _towerSelectionService;
    private ITowerBuildService _towerBuildService;

    [Inject]
    public void Construct(ITowerSelectionService towerSelectionService, ITowerBuildService towerBuildService)
    {
        _towerSelectionService = towerSelectionService;
        _towerBuildService = towerBuildService;
    }


    public void Upgrade()
    {
        if (_towerSelectionService.Tower) _towerBuildService.UpgradeTower(_towerSelectionService.Tower);
    }

    public void Sell()
    {
        if (_towerSelectionService.Tower) _towerBuildService.SellTower(_towerSelectionService.Tower);
    }

}

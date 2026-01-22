using Gameplay.Towers;
using Infrastructure.Services;
using UnityEngine;
using VContainer;

public class TowerControllerUI : MonoBehaviour
{
    private TowerController _tower;

    private ITowerBuildService _towerBuildService;

    [Inject]
    public void Construct(ITowerBuildService towerBuildService)
    {
        _towerBuildService = towerBuildService;
    }

    private void OnEnable()
    {
        Messenger<TowerController>.AddListener(Events.TowerClicked, SetTower);
    }

    private void OnDisable()
    {
        Messenger<TowerController>.RemoveListener(Events.TowerClicked, SetTower);
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
        if (_tower) _towerBuildService.SellTower(_tower);
    }

}

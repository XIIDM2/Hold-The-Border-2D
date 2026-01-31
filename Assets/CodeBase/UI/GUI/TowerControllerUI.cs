using Cysharp.Threading.Tasks;
using Gameplay.Towers;
using Infrastructure.Managers;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class TowerControllerUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _upgradeText;
    [SerializeField] private TMP_Text _sellText;

    private ITowerSelectionService _towerSelectionService;
    private ITowerBuildService _towerBuildService;

    [Inject]
    public void Construct(ITowerSelectionService towerSelectionService, ITowerBuildService towerBuildService)
    {
        _towerSelectionService = towerSelectionService;
        _towerBuildService = towerBuildService;
    }

    private void Start()
    {
        HideController();
    }

    private void OnEnable()
    {
        _towerSelectionService.TowerSelected += ShowController;
        _towerSelectionService.TowerDeselected += HideController;
    }

    private void OnDisable()
    {
        _towerSelectionService.TowerSelected -= ShowController;
        _towerSelectionService.TowerDeselected -= HideController;
    }


    private void ShowController(TowerController tower)
    {
        _upgradeText.text = tower.currentTierConfig.UpgradePrice.ToString();
        _sellText.text = tower.currentTierConfig.SellPrice.ToString();

        Vector3 position = Camera.main.WorldToScreenPoint(tower.transform.position);
        _panel.transform.position = position;

        _panel.SetActive(true);
    }

    private void HideController()
    {
        _panel.SetActive(false);
    }

    public void Upgrade()
    {
        if (_towerSelectionService.Tower)
        {
            _towerBuildService.UpgradeTower(_towerSelectionService.Tower);
            _towerSelectionService.ClearTowerSelection();
        }
    }

    public void Sell()
    {
        if (_towerSelectionService.Tower)
        {
            _towerBuildService.SellTower(_towerSelectionService.Tower);
            _towerSelectionService.ClearTowerSelection();
        }
    }

}

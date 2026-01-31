using Data;
using Gameplay.Towers.BuildSite;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class TowerBuildingControllerUI : MonoBehaviour
{
    [SerializeField] private GameObject _buildingPanel;

    private GameplayRegistry _registry;
    private SceneController _controller;
    private ITowerSelectionService _selectionService;
    private IUIFactory _UIfactory;

    [Inject]
    public void Construct(GameplayRegistry registry, SceneController controller, ITowerSelectionService selectionService, IUIFactory UIFactory)
    {
        _registry = registry;
        _controller = controller;
        _selectionService = selectionService;
        _UIfactory = UIFactory;
    }

    private async void Awake()
    {
        foreach (TowerData towerData in _registry.TowerDatas)
        {
            GameObject towerPanelUI = await _UIfactory.CreateTowerPanel(towerData.Type, towerData.Icon, towerData.Name, towerData.Description, towerData.TierConfigs[0].Damage.ToString(), towerData.TierConfigs[0].AttackCooldown.ToString(), towerData.TierConfigs[0].AttackRadius.ToString(), towerData.BuildPrice.ToString());
            towerPanelUI.transform.SetParent(_buildingPanel.transform);
        }
    }


    private void Start()
    {
        HideBuildingPanel();
    }

    private void OnEnable()
    {
        _selectionService.BuildsiteSelected += ShowBuildingPanel;
        _selectionService.BuildSiteDeselected += HideBuildingPanel;
    }

    private void OnDisable()
    {
        _selectionService.BuildsiteSelected -= ShowBuildingPanel;
        _selectionService.BuildSiteDeselected -= HideBuildingPanel;

    }  

    private void ShowBuildingPanel(BuildSite site)
    {
        _buildingPanel.SetActive(true);
        _controller.StopTime();
    }

    public void HideBuildingPanel()
    {
        _buildingPanel.SetActive(false);
        _controller.StartTime();
    }
}

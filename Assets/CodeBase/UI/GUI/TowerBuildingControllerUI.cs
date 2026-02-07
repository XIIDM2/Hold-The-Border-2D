using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Towers.BuildSite;
using Infrastructure;
using Infrastructure.Factories;
using System.Threading;
using UnityEngine;

namespace Gameplay.UI
{
    public class TowerBuildingControllerUI : MonoBehaviour
    {
        [SerializeField] private GameObject _buildingPanel;

        private SceneController _controller;

        public void Init(SceneController controller)
        {
            _controller = controller;
            HideBuildingPanel();
        }

        public async UniTask CreateTowerPanels(TowerData towerData, IUIFactory UIFactory, CancellationToken cancellationToken)
        {
            TowerPanelUI towerPanelUI = await UIFactory.CreateTowerPanel
            (
                towerData.Type, towerData.Icon, towerData.Name, towerData.Description,
                towerData.TierConfigs[0].Damage.ToString(),
                towerData.TierConfigs[0].AttackCooldown.ToString(),
                towerData.TierConfigs[0].AttackRadius.ToString(),
                towerData.BuildPrice.ToString(),
                cancellationToken
            );

            towerPanelUI.gameObject.transform.SetParent(_buildingPanel.transform);
        }

        public void ShowBuildingPanel(BuildSite site)
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
}
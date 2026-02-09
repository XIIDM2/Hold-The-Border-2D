using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class TowerBuildingView : MonoBehaviour
    {
        public GameObject BuildingPanel => _buildingPanel;
        public Button ClosePanelButton => _closePanelButton;

        [SerializeField] private GameObject _buildingPanel;
        [SerializeField] private Button _closePanelButton;

        private void Start()
        {
            HideBuildingPanel();
        }

        public void ShowBuildingPanel()
        {
            _buildingPanel.SetActive(true);
        }

        public void HideBuildingPanel()
        {
            _buildingPanel.SetActive(false);
        }
    }
}
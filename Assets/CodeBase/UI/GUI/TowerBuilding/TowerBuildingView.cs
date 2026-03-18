using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class TowerBuildingView : MonoBehaviour
    {
        public event UnityAction PanelCloseRequested;
        public GameObject BuildingPanel => _buildingPanel;

        [SerializeField] private GameObject _buildingPanel;
        [SerializeField] private Button _closePanelButton;

        private void OnEnable()
        {
            _closePanelButton.onClick.AddListener(OnClosePanelButtonClicked);
        }

        private void OnDisable()
        {
            _closePanelButton.onClick.RemoveListener(OnClosePanelButtonClicked);
        }

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

        private void OnClosePanelButtonClicked()
        {
            PanelCloseRequested?.Invoke();
        }
    }
}
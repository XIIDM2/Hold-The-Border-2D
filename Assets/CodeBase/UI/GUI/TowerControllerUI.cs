using Gameplay.Towers;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Gameplay.UI
{
    public class TowerControllerUI : MonoBehaviour
    {
        [Header("Upgrade/Sell Panel")]
        [SerializeField] private GameObject _controllerPanel;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private TMP_Text _upgradeText;
        [SerializeField] private TMP_Text _sellText;

        [Header("UpgradeInformationPanel")]
        [SerializeField] private GameObject _informationPanel;

        [SerializeField] private TMP_Text _damageOldText;
        [SerializeField] private TMP_Text _damageNewText;

        [SerializeField] private TMP_Text _attackSpeedOldText;
        [SerializeField] private TMP_Text _attackSpeedNewText;

        [SerializeField] private TMP_Text _attackRangeOldText;
        [SerializeField] private TMP_Text _attackRangeNewText;

        private TowerController _tower;

        private ITowerSelectionService _selectionService;
        private ITowerBuildService _buildService;

        [Inject]
        public void Construct(ITowerSelectionService selectionService, ITowerBuildService buildService)
        {
            _selectionService = selectionService;
            _buildService = buildService;
        }

        private void Start()
        {
            HideController();
            HideUpgradePanel();
        }

        private void OnEnable()
        {
            _selectionService.TowerSelected += ShowController;
            _selectionService.TowerDeselected += HideController;
            _selectionService.TowerDeselected += HideUpgradePanel;
        }

        private void OnDisable()
        {
            _selectionService.TowerSelected -= ShowController;
            _selectionService.TowerDeselected -= HideController;
            _selectionService.TowerDeselected -= HideUpgradePanel;
        }


        private void ShowController(TowerController tower)
        {
            _tower = tower;

            _upgradeText.text = tower.currentTierConfig.UpgradePrice.ToString();
            _sellText.text = tower.currentTierConfig.SellPrice.ToString();

            if (tower.CurrentTierIndex >= tower.MaxTier) _upgradeButton.gameObject.SetActive(false);

            Vector3 position = Camera.main.WorldToScreenPoint(tower.transform.position);
            _controllerPanel.transform.position = position;

            _controllerPanel.SetActive(true);
        }

        private void HideController()
        {
            _controllerPanel.SetActive(false);
        }

        public void ShowUpgradePanel()
        {
            _damageOldText.text = _tower.currentTierConfig.Damage.ToString();
            _damageNewText.text = _tower.nextTierConfig.Damage.ToString();

            _attackSpeedOldText.text = _tower.currentTierConfig.AttackCooldown.ToString();
            _attackSpeedNewText.text = _tower.nextTierConfig.AttackCooldown.ToString();

            _attackRangeOldText.text = _tower.currentTierConfig.AttackRadius.ToString();
            _attackRangeNewText.text = _tower.nextTierConfig.AttackRadius.ToString();

            _informationPanel.SetActive(true);
        }

        public void HideUpgradePanel()
        {
            _informationPanel.SetActive(false);
        }

        public void Upgrade()
        {
            if (_selectionService.Tower)
            {
                _buildService.UpgradeTower(_selectionService.Tower);
                _selectionService.ClearTowerSelection();
            }
        }

        public void Sell()
        {
            if (_selectionService.Tower)
            {
                _buildService.SellTower(_selectionService.Tower);
                _selectionService.ClearTowerSelection();
            }
        }

    }
}
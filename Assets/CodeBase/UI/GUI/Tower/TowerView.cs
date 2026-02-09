using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class TowerView : MonoBehaviour
    {
        public event UnityAction UpgradeRequested;
        public event UnityAction SellRequested;
        public event UnityAction UpgradeButtonHowevered;

        public Button UpgradeButton => _upgradeButton;
        public Button SellButton => _sellButton;

        [Header("Upgrade/Sell Panel")]
        [SerializeField] private GameObject _controllerPanel;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _sellButton;
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


        private void Start()
        {
            HideTowerPanel();
            HideUpgradePanel();
        }

        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(OnUpgradeClicked);
            _sellButton.onClick.AddListener(OnSellClicked);
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(OnUpgradeClicked);
            _sellButton.onClick.RemoveListener(OnSellClicked);
        }

        public void ShowTowerPanel(string upgradePrice, string sellPrice, Vector2 displayPosition)
        {
            _upgradeText.text = upgradePrice;
            _sellText.text = sellPrice;

            Vector3 position = Camera.main.WorldToScreenPoint(displayPosition);
            _controllerPanel.transform.position = position;

            _controllerPanel.SetActive(true);
        }

        public void HideTowerPanel()
        {
            _controllerPanel.SetActive(false);
        }

        public void ShowUpgradePanel(string oldDamage, string newDamage, string oldCooldown, string newCooldown, string oldRadius, string newRadius)
        {
            _damageOldText.text = oldDamage;
            _damageNewText.text = newDamage;

            _attackSpeedOldText.text = oldCooldown;
            _attackSpeedNewText.text = newCooldown;

            _attackRangeOldText.text = oldRadius;
            _attackRangeNewText.text = newRadius;

            _informationPanel.SetActive(true);
        }

        public void HideUpgradePanel()
        {
            _informationPanel.SetActive(false);
        }

        public void OnUpgradeClicked()
        {
            UpgradeRequested?.Invoke();
        }

        public void OnSellClicked()
        {
            SellRequested?.Invoke();
        }

        public void OnUpgradeButtonHovered()
        {
            UpgradeButtonHowevered?.Invoke();
        }

    }
}
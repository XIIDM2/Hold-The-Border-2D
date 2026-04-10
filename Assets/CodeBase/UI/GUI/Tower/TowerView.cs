using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class TowerView : MonoBehaviour
    {
        public event UnityAction ControllerPointerExit;

        public event UnityAction UpgradeRequested;
        public event UnityAction SellRequested;

        public event UnityAction UpgradeButtonPointerEnter;
        public event UnityAction UpgradeButtonPointerExit;

        public event UnityAction<int> StrategyChanged;

        public Button UpgradeButton => _upgradeButton;


        [SerializeField] private GameObject _controllerPanel;

        [Header("Upgrade/Sell Panel")]
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _sellButton;
        [SerializeField] private TMP_Text _upgradeText;
        [SerializeField] private TMP_Text _sellText;

        [SerializeField] private GameObject _toolTip;
        [SerializeField] private TMP_Text _toolTipText;

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
            HideToolTip();
        }

        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
            _sellButton.onClick.AddListener(OnSellButtonClicked);
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
            _sellButton.onClick.RemoveListener(OnSellButtonClicked);
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

        public void ShowToolTip(string text)
        {
            _toolTipText.text = text;
            _toolTip.SetActive(true);
        }

        public void HideToolTip()
        {
            _toolTip.SetActive(false);
        }

        public void OnControllerPointerExit()
        {
            ControllerPointerExit?.Invoke();
        }

        public void OnUpgradeButtonClicked()
        {
            UpgradeRequested?.Invoke();
        }

        public void OnSellButtonClicked()
        {
            SellRequested?.Invoke();
        }

        public void OnUpgradeButtonPointerEnter()
        {
            UpgradeButtonPointerEnter?.Invoke();
        }

        public void OnUpgradeButtonPointerExit()
        {
            UpgradeButtonPointerExit?.Invoke();
        }

        public void OnStrategyButtonClicked(int strategyIndex)
        {
            StrategyChanged?.Invoke(strategyIndex);
        }
    }
}
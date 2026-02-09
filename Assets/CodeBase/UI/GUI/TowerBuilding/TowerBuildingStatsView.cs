using Gameplay.Towers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class TowerBuildingStatsView : MonoBehaviour
    {
        public event UnityAction<TowerType> BuildRequested;
        public Button BuildButton => _buildButton;

        [SerializeField] private Image _icon;

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;

        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _attackCooldownText;
        [SerializeField] private TMP_Text _attackRadiusText;
        [SerializeField] private TMP_Text _buildPriceText;

        [SerializeField] private Button _buildButton;

        private TowerType _type;

        public void Init(TowerType type, Sprite icon, string name, string description, string damage, string attackCooldown, string attackRadius, string price)
        {
            _type = type;
            _icon.sprite = icon;
            _nameText.text = name;
            _descriptionText.text = description;
            _damageText.text = damage;
            _attackCooldownText.text = attackCooldown;
            _attackRadiusText.text = attackRadius;
            _buildPriceText.text = price;
        }

        private void OnEnable()
        {
            _buildButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _buildButton.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            BuildRequested?.Invoke(_type);
        }
    }
}
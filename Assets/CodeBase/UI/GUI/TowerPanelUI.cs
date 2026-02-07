using Gameplay.Towers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class TowerPanelUI : MonoBehaviour
    {
        public TowerBuildButton BuildButton => _buildButton;

        [SerializeField] private Image _icon;

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;

        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _attackCooldownText;
        [SerializeField] private TMP_Text _attackRadiusText;
        [SerializeField] private TMP_Text _buildPriceText;

        [SerializeField] private TowerBuildButton _buildButton;

        public void Init(TowerType type, Sprite icon, string name, string description, string damage, string attackCooldown, string attackRadius, string price)
        {
            _icon.sprite = icon;
            _nameText.text = name;
            _descriptionText.text = description;
            _damageText.text = damage;
            _attackCooldownText.text = attackCooldown;
            _attackRadiusText.text = attackRadius;
            _buildPriceText.text = price;

            _buildButton.SetTowerType(type);
        }
    }
}
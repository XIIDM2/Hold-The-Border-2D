using Gameplay.Towers;
using Gameplay.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerPanelUI : MonoBehaviour
{
    public TowerBuildButton BuildButton => _buildButton;

    [SerializeField] private Image _towerIcon;

    [SerializeField] private TMP_Text _towerNameText;
    [SerializeField] private TMP_Text _descriptionText;

    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _attackCooldownText;
    [SerializeField] private TMP_Text _attackRadiusText;
    [SerializeField] private TMP_Text _priceText;

    [SerializeField] private TowerBuildButton _buildButton;

    public void Init(TowerType type, Sprite icon, string name, string description, string damage, string attackCooldown, string attackRadius, string price)
    {
        _towerIcon.sprite = icon;
        _towerNameText.text = name;
        _descriptionText.text = description;
        _damageText.text = damage;
        _attackCooldownText.text = attackCooldown;
        _attackRadiusText.text = attackRadius;
        _priceText.text = price;

        _buildButton.SetTowerType(type);
    }
}

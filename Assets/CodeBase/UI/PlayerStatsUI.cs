using TMPro;
using UnityEngine;
using VContainer;

public class PlayerStatsUI : MonoBehaviour
{
    private IPlayerController _player;

    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _goldText;

    [Inject]
    public void Contstruct(IPlayerController player)
    {
        _player = player;
    }

    private void Start()
    {
        SetHealthText(_player.Health.CurrentHealth);
        SetGoldText(_player.Gold);
    }

    private void OnEnable()
    {
        _player.Health.OnHealthChanged += SetHealthText;
        _player.OnGoldChanged += SetGoldText;
    }

    private void OnDisable()
    {
        _player.Health.OnHealthChanged -= SetHealthText;
        _player.OnGoldChanged -= SetGoldText;
    }

    private void SetHealthText(int health)
    {
        _healthText.text = health.ToString();
    }

    private void SetGoldText(int gold)
    {
        _goldText.text = gold.ToString();
    }


}

using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _goldText;

        public void Init(int health, int gold)
        {
            OnHealthChanged(health);
            OnGoldChanged(gold);
        }

        public void OnHealthChanged(int health)
        {
            _healthText.text = health.ToString();
        }

        public void OnGoldChanged(int gold)
        {
            _goldText.text = gold.ToString();
        }
    }
}
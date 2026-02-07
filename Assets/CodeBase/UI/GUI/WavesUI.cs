using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class WavesUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _wavesText;

        private int _maxWavesLength;

        public void Init(int currentWaveIndex, int maxWavesLength)
        {
            _maxWavesLength = maxWavesLength;
            OnNextWaveStarted(currentWaveIndex);
        }

        public void OnNextWaveStarted(int currentWaveIndex)
        {
            _wavesText.text = $"{currentWaveIndex}/{_maxWavesLength}";
        }
    }
}
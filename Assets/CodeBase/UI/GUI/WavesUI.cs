using Infrastructure.Services;
using TMPro;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class WavesUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _wavesText;

        private IWaveControllerService _waveControllerService;

        [Inject]
        public void Construct(IWaveControllerService waveControllerService)
        {
            _waveControllerService = waveControllerService;
        }

        private void Start()
        {
            OnNextWaveStarted(_waveControllerService.CurrentWaveIndex);
        }

        private void OnEnable()
        {
            _waveControllerService.NextWaveStarted += OnNextWaveStarted;
        }

        private void OnDisable()
        {
            _waveControllerService.NextWaveStarted -= OnNextWaveStarted;
        }

        private void OnNextWaveStarted(int currentWaveIndex)
        {
            _wavesText.text = $"{currentWaveIndex}/{_waveControllerService.WavesLength}";
        }
    }
}
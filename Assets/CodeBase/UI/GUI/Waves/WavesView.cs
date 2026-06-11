using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class WavesView : MonoBehaviour
    {
        public event UnityAction WavesStartRequested;
        public event UnityAction WavesTimerSkipRequested;

        [SerializeField] private GameObject _buttonsPanel;

        [SerializeField] private TMP_Text _wavesText;

        [SerializeField] private Button _startWavesButton;
        [SerializeField] private Button _skipTimerButton;
        [SerializeField] private TMP_Text _skipTimerText;
        private Image _skipTimerImage;

        private int _maxWavesLength;
        private Sequence _sequence;

        private void Awake()
        {
            _sequence = DOTween.Sequence();
        }

        private void OnEnable()
        {
            _startWavesButton.onClick.AddListener(OnStartWaveButtonClicked);
            _skipTimerButton.onClick.AddListener(OnWaveTimerSkipButtonClicked);
        }

        private void OnDisable()
        {
            _startWavesButton.onClick.RemoveListener(OnStartWaveButtonClicked);
            _skipTimerButton.onClick.RemoveListener(OnWaveTimerSkipButtonClicked);
        }

        public void Init(int currentWaveIndex, int maxWavesLength)
        {
            _maxWavesLength = maxWavesLength;
            OnNextWaveStarted(currentWaveIndex);

            _skipTimerImage = _skipTimerButton.GetComponent<Image>();

            _skipTimerImage.DOFade(0.6f, 0);
            _skipTimerText.DOFade(0.6f, 0);

            _sequence.Append(_skipTimerImage.DOFade(1f, 1f)).Join(_skipTimerText.DOFade(1f, 1f)).SetLink(gameObject, LinkBehaviour.KillOnDisable).SetAutoKill(false).Pause();

            _skipTimerImage.gameObject.SetActive(false);
        }

        public void ShowButtonsPanel()
        {
            _buttonsPanel.SetActive(true);
        }

        public void HideButtonsPanel()
        {
            _buttonsPanel.SetActive(false);
        }

        public void HighlightSkipButton()
        {
            _sequence.PlayForward();
        }

        public void HideSkipButton()
        {
            _sequence.PlayBackwards();
        }

        public void OnNextWaveStarted(int currentWaveIndex)
        {
            _wavesText.text = $"{currentWaveIndex}/{_maxWavesLength}";
            _skipTimerButton.gameObject.SetActive(false);
        }

        public void OnNextWaveTimerTicked(float timeLeft)
        {
            _skipTimerText.text = timeLeft.ToString("F0");
        }

        public void OnWaveFinished()
        {
            _skipTimerButton.gameObject.SetActive(true);
        }

        public void OnStartWaveButtonClicked()
        {
            WavesStartRequested?.Invoke();
            _startWavesButton.gameObject.SetActive(false);
        }

        public void OnWaveTimerSkipButtonClicked()
        {
            WavesTimerSkipRequested?.Invoke();
            _skipTimerButton.gameObject.SetActive(false);
        }

    }
}
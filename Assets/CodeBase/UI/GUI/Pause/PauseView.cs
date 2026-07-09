using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class PauseView : MonoBehaviour
    {
        public event UnityAction PauseRequested;
        public event UnityAction ContinueRequested;
        public event UnityAction RestartRequested;
        public event UnityAction SettingsRequested;
        public event UnityAction MainMenuRequested;

        [SerializeField] private GameObject _dimmingPanel;
        [SerializeField] private GameObject _pausePanel;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _mainMenuButton;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }

        private void Start()
        {
            _pausePanel.SetActive(false);
        }

        public void ShowPanel()
        {
            _dimmingPanel.SetActive(true);
            _pausePanel.SetActive(true);
        }

        public void HidePanel()
        {
            _dimmingPanel.SetActive(false);
            _pausePanel.SetActive(false);
        }

        public void HidePauseButton()
        {
            _pauseButton.gameObject.SetActive(false);
        }

        private void OnPauseButtonClicked()
        {
            PauseRequested?.Invoke();
        }

        private void OnContinueButtonClicked()
        {
            ContinueRequested?.Invoke();
        }

        private void OnRestartButtonClicked()
        {
            RestartRequested?.Invoke();
        }

        private void OnSettingsButtonClicked()
        {
            SettingsRequested?.Invoke();
        }

        private void OnMainMenuButtonClicked()
        {
            MainMenuRequested?.Invoke();
        }
    }
}
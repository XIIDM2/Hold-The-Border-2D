using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class MainMenuView : MonoBehaviour
    {
        public event UnityAction StartRequested;
        public event UnityAction ReturnToMenuRequested;
        public event UnityAction SettingsRequested;
        public event UnityAction ExitRequested;
        public GameObject LevelPanel => _levelPanel;

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _returnToMenuButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _levelPanel;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClicked);
            _returnToMenuButton.onClick.AddListener(OnReturnToMenuRequested);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);

        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClicked);
            _returnToMenuButton.onClick.RemoveListener(OnReturnToMenuRequested);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        public void ShowLevelPanel()
        {
            _mainMenuPanel.SetActive(false);
            _levelPanel.SetActive(true);
        }

        public void HideLevelPanel()
        {
            _levelPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
        }

        private void OnStartButtonClicked()
        {
            StartRequested?.Invoke();
        }

        private void OnReturnToMenuRequested()
        {
            ReturnToMenuRequested?.Invoke();
        }

        private void OnSettingsButtonClicked()
        {
            SettingsRequested?.Invoke();
        }

        private void OnExitButtonClicked()
        {
            ExitRequested?.Invoke();
        }
    }
}
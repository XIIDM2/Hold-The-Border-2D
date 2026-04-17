using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class MainMenuView : MonoBehaviour
    {
        public event UnityAction StartRequested;
        public event UnityAction SettingsRequested;
        public event UnityAction ExitRequested;


        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);

        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            StartRequested?.Invoke();
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
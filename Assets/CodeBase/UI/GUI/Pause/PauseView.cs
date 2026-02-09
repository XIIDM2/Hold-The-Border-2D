using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class PauseView : MonoBehaviour
    {
        public Button PauseButton => _pauseButton;
        public Button ContinueButton => _continueButton;
        public Button RestartButton => _restartButton;
        public Button SettingsButton => _settingsButton;
        public Button MainMenuButton => _mainMenuButton;

        [SerializeField] private GameObject _pausePanel;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _mainMenuButton;
   
        private void Start()
        {
            _pausePanel.SetActive(false);
        }

        public void ShowPanel()
        {
            _pausePanel.SetActive(true);
        }

        public void HidePanel()
        {
            _pausePanel.SetActive(false);
        }
    }
}
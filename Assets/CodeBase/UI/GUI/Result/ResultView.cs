using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class ResultView : MonoBehaviour
    {
        public event UnityAction RestartRequested;
        public event UnityAction MainMenuRequested;

        [SerializeField] private GameObject _resultPanel;

        [SerializeField] private TMP_Text _resultText;

        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _maiMenuButton;

        private void Start()
        {
            _resultPanel.SetActive(false);
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _maiMenuButton.onClick.AddListener(OnMainMenuBUttonClicked);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _maiMenuButton.onClick.RemoveListener(OnMainMenuBUttonClicked);
        }

        public void ShowVictoryPanel()
        {
            _resultText.text = "Victory!";
            _resultPanel.SetActive(true);
        }

        public void ShowDefeatPanel()
        {
            _resultText.text = "Defeat!";
            _resultPanel.SetActive(true);
        }

        public void HidePanel()
        {
            _resultPanel.SetActive(false);
        }

        private void OnRestartButtonClicked()
        {
            RestartRequested?.Invoke(); 
        }

        private void OnMainMenuBUttonClicked()
        {
            MainMenuRequested?.Invoke();
        }
    }
}
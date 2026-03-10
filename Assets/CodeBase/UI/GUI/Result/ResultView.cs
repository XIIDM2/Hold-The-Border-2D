using Infrastructure.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class ResultView : MonoBehaviour
    {
        public Button RestartButton => _restartButton;
        public Button MainMenuButton => _maiMenuButton;

        [SerializeField] private GameObject _resultPanel;

        [SerializeField] private TMP_Text _resultText;

        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _maiMenuButton;

        private void Start()
        {
            _resultPanel.SetActive(false);
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
    }
}
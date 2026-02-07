using Infrastructure;
using UnityEngine;

namespace Gameplay.UI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;

        private SceneController _controller;
       
        public void Init(SceneController controller)
        {
            _controller = controller;
            _pausePanel.SetActive(false);
        }

        public void PauseGame()
        {
            _controller.StopTime();
            _pausePanel.SetActive(true);
        }

        public void ContinueGame()
        {
            _controller.StartTime();
            _pausePanel.SetActive(false);
        }

        public void RestartLevel()
        {
            _controller.StartTime();
            _controller.RestartScene();
        }

        public void ExitToMainMenu()
        {
            _controller.LoadMainMenuScene();
        }
    }
}
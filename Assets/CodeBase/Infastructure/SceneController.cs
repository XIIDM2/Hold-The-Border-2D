using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Infrastructure
{
    public class SceneController : IInitializable
    {
        private const string MAIN_MENU_SCENE_NAME = "Main Menu";
        private const string HUD_SCENE_NAME = "HUD";

        public void Initialize()
        {
            if (SceneManager.GetActiveScene().name == MAIN_MENU_SCENE_NAME) return;

            SceneManager.LoadScene(HUD_SCENE_NAME, LoadSceneMode.Additive);
        }


        public void StartTime()
        {
            Time.timeScale = 1.0f;
        }

        public void StopTime()
        {
            Time.timeScale = 0.0f;
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
        }

        public void Exit()
        {
            Application.Quit();
        }

    }
}
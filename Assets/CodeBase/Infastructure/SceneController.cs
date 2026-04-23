using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Gameplay.UI;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Infrastructure
{
    public class SceneController
    {
        private const string MAIN_MENU_SCENE_NAME = "Main Menu";
        private const string LOADING_SCENE_NAME = "Loading";
        private const string HUD_SCENE_NAME = "HUD";

        private const float FAKE_LOADING_TIME = 3.0f;

        private readonly IAssetProviderService _providerService;

        private Fader _faderPrefab;

        public SceneController(IAssetProviderService providerService, Fader faderPrefab)
        {
            _providerService = providerService;
            _faderPrefab = faderPrefab;
        }

        public void StartTime()
        {
            Time.timeScale = 1.0f;
        }

        public void StopTime()
        {
            Time.timeScale = 0.0f;
        }

        public async UniTask ChangeScene(int sceneBuildIndex, CancellationToken token=default)
        {
            Fader fader = GameObject.Instantiate(_faderPrefab);

            await fader.FadeSceen();

            await SceneManager.LoadSceneAsync(LOADING_SCENE_NAME);

            await fader.UnFadeScreen();

            _providerService.ReleaseAllAssets();

            await _providerService.LoadMultipleAssetsByLabel("Level_1", token);

            await UniTask.WaitForSeconds(FAKE_LOADING_TIME);

            await fader.FadeSceen();

            await SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);

            SceneManager.LoadScene(HUD_SCENE_NAME, LoadSceneMode.Additive);

            await fader.UnFadeScreen();

            GameObject.Destroy(fader);
        }

        public void RestartScene()
        {
            StartTime();
            ChangeScene(SceneManager.GetActiveScene().buildIndex).Forget();

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
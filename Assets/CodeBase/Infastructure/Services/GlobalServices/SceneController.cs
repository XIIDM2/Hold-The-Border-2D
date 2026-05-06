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

        private string _lastLoadedLabel = null;

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

        public async UniTask ChangeScene(string sceneName, string addressablesLabel=null, bool loadHUD=true, CancellationToken token=default)
        {
            Fader fader = GameObject.Instantiate(_faderPrefab);

            await fader.FadeSceen();

            await SceneManager.LoadSceneAsync(LOADING_SCENE_NAME);

            await fader.UnFadeScreen();

            _providerService.ReleaseAllAssets();

            if (addressablesLabel != null)
            {
                await _providerService.LoadMultipleAssetsByLabel(addressablesLabel, token);
                _lastLoadedLabel = addressablesLabel;
            }

            await UniTask.WaitForSeconds(FAKE_LOADING_TIME);

            await fader.FadeSceen();

            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            if (loadHUD) SceneManager.LoadScene(HUD_SCENE_NAME, LoadSceneMode.Additive);

            await fader.UnFadeScreen();

            GameObject.Destroy(fader.gameObject);
        }

        public void RestartScene()
        {
            StartTime();
            ChangeScene(SceneManager.GetActiveScene().name, _lastLoadedLabel).Forget();

        }

        public void LoadMainMenuScene()
        {
            StartTime();
            ChangeScene(MAIN_MENU_SCENE_NAME, loadHUD : false).Forget();
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
using Core.Interfaces;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Infrastructure
{
    public class SceneController : IAsyncStartable
    {
        private const string MAIN_MENU_SCENE_NAME = "Main Menu";
        private const string LOADING_SCENE_NAME = "Loading";
        private const string HUD_SCENE_NAME = "HUD";

        private readonly IAssetProviderService _providerService;

        public SceneController(IAssetProviderService providerService)
        {
            _providerService = providerService;
        }

        public async UniTask StartAsync(CancellationToken cancellation = default)
        {
            await ChangeScene(1, cancellation);
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
            await SceneManager.LoadSceneAsync(LOADING_SCENE_NAME);

            _providerService.ReleaseAllAssets();

            await _providerService.LoadMultipleAssetsByLabel("Level_1", token);

            await SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);

            SceneManager.LoadScene(HUD_SCENE_NAME, LoadSceneMode.Additive);
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
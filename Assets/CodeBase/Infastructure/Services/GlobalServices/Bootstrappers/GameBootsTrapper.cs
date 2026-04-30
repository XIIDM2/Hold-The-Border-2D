using VContainer.Unity;

namespace Infrastructure.Services.Bootstrappers
{
    public class GameBootsTrapper : IStartable
    {
        private readonly IAudioService _audioService;
        private readonly ISettingsService _settingsService;
        private readonly SceneController _sceneController;
        public GameBootsTrapper(IAudioService audioService, ISettingsService settingsService, SceneController sceneController)
        {
            _audioService = audioService;
            _settingsService = settingsService;
            _sceneController = sceneController;
        }

        public void Start()
        {
            _audioService.Init();
            _settingsService.Init();

            _sceneController.LoadMainMenuScene();
        }
    }
}
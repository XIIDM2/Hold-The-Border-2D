using Infrastructure.Events;
using System;
using UnityEngine.Events;
using VContainer;

namespace Infrastructure.Services
{
    public class LevelService : ILevelService, IDisposable
    {
        private IEventBus _eventBus;
        private SceneController _sceneController;

        public event UnityAction Victory;
        public event UnityAction Defeat;

        [Inject]
        public void Construct(IEventBus eventBus, SceneController sceneController)
        {
            _eventBus = eventBus;
            _sceneController = sceneController;
        }

        public void Init()
        {
            _eventBus.Subscribe<PlayerDiedEvent>(OnPlayerDeath);
            _eventBus.Subscribe<AllEnemiesKilledEvent>(OnAllEnemiesKilled);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<PlayerDiedEvent>(OnPlayerDeath);
            _eventBus.Unsubscribe<AllEnemiesKilledEvent>(OnAllEnemiesKilled);
        }

        private void OnPlayerDeath(PlayerDiedEvent _)
        {
            _sceneController.StopTime();
            Defeat?.Invoke();
        }

        private void OnAllEnemiesKilled(AllEnemiesKilledEvent _)
        {
            _sceneController.StopTime();
            Victory?.Invoke();
        }
    }
}
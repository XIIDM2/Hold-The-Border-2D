using Data;
using Infrastructure.Events;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VContainer;

namespace Infrastructure.Services
{
    public class LevelService : ILevelService, IDisposable
    {
        private IEventBus _eventBus;
        private SFXRegistry _SFXRegistry;
        private SceneController _sceneController;

        private Physics2DRaycaster _physics2DRaycaster;

        public event UnityAction Victory;
        public event UnityAction Defeat;

        [Inject]
        public void Construct(IEventBus eventBus, SceneController sceneController, SFXRegistry sFXRegistry, Physics2DRaycaster physics2DRaycaster)
        {
            _eventBus = eventBus;
            _sceneController = sceneController;
            _SFXRegistry = sFXRegistry;
            _physics2DRaycaster = physics2DRaycaster;
        }

        public void Init()
        {
            _physics2DRaycaster.enabled = true;

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
            _physics2DRaycaster.enabled = false;
            _eventBus.Publish(new InvokeSFX(_SFXRegistry.DefeatSound));
            Defeat?.Invoke();
        }

        private void OnAllEnemiesKilled(AllEnemiesKilledEvent _)
        {
            _sceneController.StopTime();
            _physics2DRaycaster.enabled = false;
            _eventBus.Publish(new InvokeSFX(_SFXRegistry.VictorySound));
            Victory?.Invoke();
        }
    }
}
using Data;
using Infrastructure.Events;
using UnityEngine;
using VContainer.Unity;

namespace Infrastructure.Services.Bootstrappers
{
    public class MainMenuBootsTrapper : IStartable
    {
        private readonly IEventBus _eventBus;

        private AudioClip _music;

        public MainMenuBootsTrapper(IEventBus eventBus, SFXRegistry SFXRegistry)
        {
            _eventBus = eventBus;
            _music = SFXRegistry.MainMenuMusic;
        }

        public void Start()
        {
            _eventBus.Publish(new LevelStartedEvent(_music));
        }
    }
}
using Infrastructure.Services;
using UnityEngine;
using VContainer;
using Infrastructure.Events;

namespace Gameplay.Towers
{
    public class TowerAudio : MonoBehaviour
    {
        private AudioClip _attackSound;
        private AudioClip _buildSound;

        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Init(AudioClip buildSound, AudioClip attackSound)
        {
            _buildSound = buildSound;
            _attackSound = attackSound;
        }

        public void PlayBuildSound()
        {
            _eventBus.Publish(new InvokeSFX(_buildSound));
        }


        public void OnAttack()
        {
            _eventBus.Publish(new InvokeSFX(_attackSound));
        }
    }
}
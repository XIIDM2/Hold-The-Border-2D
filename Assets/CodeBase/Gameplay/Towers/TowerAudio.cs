using Infrastructure.Services;
using UnityEngine;
using VContainer;
using Infrastructure.Events;

public class TowerAudio : MonoBehaviour
{
    private AudioClip _attackSound;

    private IEventBus _eventBus;

    [Inject]
    public void Construct(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void Init(AudioClip attackSound)
    {
        _attackSound = attackSound;
    }

    public void OnAttack(Transform _)
    {
        _eventBus.Publish(new InvokeSFX(_attackSound));
    }
}

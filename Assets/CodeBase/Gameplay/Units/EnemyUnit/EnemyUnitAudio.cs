using Gameplay.Units.Enemy;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using UnityEngine;
using VContainer;

public class EnemyUnitAudio : MonoBehaviour
{
    private IAudioService _audioService;
    private IDamageable _health;

    private AudioClip _hitSound;
    private AudioClip _deathSound;

    [Inject]
    public void Construct(IAudioService audioService)
    {
        _audioService = audioService;
    }

    private void Awake()
    {
        _health = GetComponent<EnemyUnitController>().Health;
    }


    private void OnEnable()
    {
        _health.HealthChanged += PlayHitSound;
        _health.Death += PlayDeathSound;

    }


    private void OnDisable()
    {
        _health.HealthChanged -= PlayHitSound;
        _health.Death -= PlayDeathSound;
    }

    public void Init(AudioClip hitSound, AudioClip deathSound)
    {
        _hitSound = hitSound;
        _deathSound = deathSound;

    }

    private void PlayHitSound(int _)
    {
        _audioService.PlaySound(_hitSound);
    }

    private void PlayDeathSound(IDamageable _)
    {
        _audioService.PlaySound(_deathSound);
    }


}

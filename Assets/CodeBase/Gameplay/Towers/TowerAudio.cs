using Infrastructure.Services;
using UnityEngine;
using VContainer;

public class TowerAudio : MonoBehaviour
{
    private AudioClip _attackSound;

    private IAudioService _audioService;

    [Inject]
    public void Construct(IAudioService audioService)
    {
        _audioService = audioService;
    }

    public void Init(AudioClip attackSound)
    {
        _attackSound = attackSound;
    }

    public void PlayAttackSound(Transform _)
    {
        _audioService.PlaySound(_attackSound);
    }
}

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Services
{
    public interface IAudioService
    {
        void Init();
        void PlaySound(AudioClip clip);
        UniTask PlayMusic(AudioClip clip);
        void PlayAmbience(AudioClip clip);

        float SFXVolume { get; set; }
        float AmbienceVolume {  get; set; }
        float MusicVolume { get; set; }

    }
}
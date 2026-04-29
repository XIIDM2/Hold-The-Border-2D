using UnityEngine;

namespace Infrastructure.Services
{
    public interface IAudioService
    {
        public void PlaySound(AudioClip clip);
        public void PlayMusic(AudioClip clip);
        public void PlayAmbience(AudioClip clip);

        float SFXVolume { get; set; }
        float AmbienceVolume {  get; set; }
        float MusicVolume { get; set; }

    }
}
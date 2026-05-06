using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SFX Registry", menuName = "Scriptable Objects/Registries/SFXRegistry")]
    public class SFXRegistry : ScriptableObject
    {
        [SerializeField] private AudioClip _mainMenuMusic;
        [SerializeField] private AudioClip _startWaveSound;
        [SerializeField] private AudioClip _victorySound;
        [SerializeField] private AudioClip _defeatSound;
        [SerializeField] private AudioClip _coinsSound;

        public AudioClip MainMenuMusic => _mainMenuMusic;
        public AudioClip StartWaveSound => _startWaveSound;
        public AudioClip VictorySound => _victorySound;
        public AudioClip DefeatSound => _defeatSound;
        public AudioClip CoinsSound => _coinsSound;
    }
}
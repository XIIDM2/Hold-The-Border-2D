using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "Scriptable Objects/Levels/Level Data")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private AudioClip _ambienceSound;
        [SerializeField] private AudioClip _music;

        public AudioClip AmbienceSound => _ambienceSound;
        public AudioClip Music => _music;
    }
}
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "ScriptableObjects/Levels/Level Data")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private string _levelName;
        [SerializeField] private string _addressablesLabel;
        [SerializeField] private AudioClip _ambienceSound;
        [SerializeField] private AudioClip _music;

        public string LevelName => _levelName;
        public string AddressablesLabel => _addressablesLabel;
        public AudioClip AmbienceSound => _ambienceSound;
        public AudioClip Music => _music;
    }
}
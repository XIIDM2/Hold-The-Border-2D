using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Objects/Player/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int _maxHeath;
        [SerializeField] private int _startGold;

        [SerializeField] private AudioClip _hitSound;

        public int MaxHeath => _maxHeath;
        public int StartGold => _startGold;
        public AudioClip HitSound => _hitSound;
    }
}
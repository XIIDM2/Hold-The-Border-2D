using UnityEngine;

namespace Gameplay.Level
{
    public class LevelConfig : MonoBehaviour
    {
        public Transform UnitSpawnPoint => _unitSpawnPoint;
        public Transform[] BuildsitePoints => _buildsitePoints;

        [Header("Level Settings")]
        [SerializeField] private Transform _unitSpawnPoint;
        [SerializeField] private Transform[] _buildsitePoints;
    }
}
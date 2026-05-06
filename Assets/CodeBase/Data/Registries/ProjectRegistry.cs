using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Project Registry", menuName = "Scriptable Objects/Registries/ProjectRegistry")]
    public class ProjectRegistry : ScriptableObject
    {
        [SerializeField] private LevelData[] _levelDatas;

        public LevelData[] LevelDatas => _levelDatas;
    }
}
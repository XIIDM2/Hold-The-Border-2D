using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SkillsRegistry", menuName = "ScriptableObjects/Registries/SkillsRegistry")]
    public class SkillRegistry : ScriptableObject
    {
        [SerializeField] private SkillData[] _skillDatas;
        public SkillData[] SkillDatas => _skillDatas;
    }
}
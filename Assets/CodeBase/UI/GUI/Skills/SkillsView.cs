using UnityEngine;

namespace Gameplay.UI
{
    public class SkillsView : MonoBehaviour
    {
        public GameObject SkillsPanel => _skillsPanel;

        [SerializeField] private GameObject _skillsPanel;
    }
}
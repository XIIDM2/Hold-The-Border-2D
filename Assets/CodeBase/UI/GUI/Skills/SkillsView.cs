using UnityEngine;

namespace Gameplay.UI
{
    public class SkillsView : MonoBehaviour
    {
        public GameObject SkillsPanel => _skillsPanel;

        [SerializeField] private GameObject _skillsPanel;

        public void ShowPanel()
        {
            _skillsPanel.SetActive(true);
        }

        public void HidePanel()
        {
            _skillsPanel.SetActive(false);
        }
    }
}
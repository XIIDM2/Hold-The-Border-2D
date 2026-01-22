using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Gameplay.Towers.BuildSite
{
    public class BuildSite : MonoBehaviour, IPointerClickHandler
    {
        public static event UnityAction<BuildSite> BuildSiteClicked;
        public void OnPointerClick(PointerEventData eventData)
        {
            BuildSiteClicked?.Invoke(this);
        }
    }
}
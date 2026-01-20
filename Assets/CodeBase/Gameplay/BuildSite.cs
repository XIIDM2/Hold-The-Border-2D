using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Towers.BuildSite
{
    public class BuildSite : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Messenger<BuildSite>.Broadcast(Events.BuildSiteClicked, this);
        }
    }
}
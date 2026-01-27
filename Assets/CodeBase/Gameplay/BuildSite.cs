using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VContainer;

namespace Gameplay.Towers.BuildSite
{
    public class BuildSite : MonoBehaviour, IPointerClickHandler
    {
        private ITowerSelectionService _selectionService;

        [Inject]
        public void Construct(ITowerSelectionService selectionService)
        {
            _selectionService = selectionService;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _selectionService.SelectBuildSite(this);
        }
    }
}
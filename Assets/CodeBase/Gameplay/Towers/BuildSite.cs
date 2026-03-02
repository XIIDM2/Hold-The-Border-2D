using DG.Tweening;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Gameplay.Towers.BuildSite
{
    public class BuildSite : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private ITowerSelectionService _selectionService;

        private Tween _highlightTween;
        private float _highlightSize = 1.3f;
        private float _highlightDuration = 0.5f;

        [Inject]
        public void Construct(ITowerSelectionService selectionService)
        {
            _selectionService = selectionService;
        }
        private void Start()
        {
            _highlightTween = transform.DOScale(_highlightSize, _highlightDuration).SetAutoKill(false).SetLink(gameObject, LinkBehaviour.KillOnDestroy).Pause();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _selectionService.SelectBuildSite(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _highlightTween.PlayForward();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _highlightTween.PlayBackwards();
        }
    }
}
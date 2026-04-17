using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class ButtonVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        [Header("Highlight Tween Settings")]
        [SerializeField] private float _sizeIncrease = 1.1f;
        [SerializeField] private float _increaseDuration = 0.2f;

        private Tween _highlightTween;

        private readonly int _textOffset = 10;

        private void Awake()
        {
            _highlightTween = _button.transform.DOScale(_sizeIncrease, _increaseDuration).SetLink(gameObject, LinkBehaviour.KillOnDestroy).SetAutoKill(false).SetUpdate(true).Pause();
        }

        private void OnDisable()
        {
            _highlightTween.Rewind();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _highlightTween.PlayForward();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _highlightTween.PlayBackwards();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_text != null) _text.transform.position = new Vector2(_text.transform.position.x, _text.transform.position.y - _textOffset);
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            if (_text != null) _text.transform.position = new Vector2(_text.transform.position.x, _text.transform.position.y + _textOffset);
        }
    }
}
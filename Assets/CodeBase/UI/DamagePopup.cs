using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class DamagePopup : MonoBehaviour
    {
        [SerializeField] private float _moveOffset = 1.5f;
        [SerializeField] private float _scaleValue = 1.5f;

        [SerializeField] private float _moveTime = 1.0f;
        [SerializeField] private float _scaleTime = 1.0f;
        [SerializeField] private float _fadeTime = 2.0f;

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            DOTween.Sequence()
                .Append(gameObject.transform.DOMoveY(transform.position.y + _moveOffset, _moveTime))
                .Join(gameObject.transform.DOScale(_scaleValue, _scaleTime))
                .Join(_text.DOFade(0, _fadeTime)
                .OnComplete(() =>
                {
                    Destroy(gameObject);
                }))
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        public void Init(int damage)
        {
            _text.text = damage.ToString();
        }
    }
}
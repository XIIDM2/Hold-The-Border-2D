using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class LoadingVisual : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _letters;

        [SerializeField] private float _sizeValue = 1.1f;
        [SerializeField] private float _duration = 0.7f;
        [SerializeField] private float _offset = 0.15f;

        private Sequence _loadingAnimationSequence;

        private void Start()
        {
            _loadingAnimationSequence = DOTween.Sequence().SetLink(gameObject, LinkBehaviour.KillOnDisable);

            for (int i = 0; i < _letters.Length; i++)
            {
                _loadingAnimationSequence.Insert(i * _offset, _letters[i].transform.DOScale(_sizeValue, _duration).SetEase(Ease.OutSine).SetLoops(int.MaxValue, LoopType.Yoyo));
            }
        }

    }
}
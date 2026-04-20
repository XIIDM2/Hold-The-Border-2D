using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration = 1.0f;

        [SerializeField] private Image _faderImage;

        private Tween _fadeTween;

        private void Awake()
        {

            _fadeTween = _faderImage.DOFade(1, _fadeDuration).SetAutoKill(false).SetLink(gameObject, LinkBehaviour.KillOnDisable).SetUpdate(true).Pause();

            DontDestroyOnLoad(gameObject);
        }

        public async UniTask FadeSceen()
        {
            _fadeTween.PlayForward();
            await _fadeTween.AsyncWaitForCompletion();
        }

        public async UniTask UnFadeScreen()
        {
            _fadeTween.PlayBackwards();
            await _fadeTween.AsyncWaitForCompletion();
        }
    }
}
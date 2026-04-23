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

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public async UniTask FadeSceen()
        {
            await _faderImage.DOFade(1, _fadeDuration).SetUpdate(true).ToUniTask();
        }

        public async UniTask UnFadeScreen()
        {
            await _faderImage.DOFade(0, _fadeDuration).SetUpdate(true).ToUniTask();
        }
    }
}
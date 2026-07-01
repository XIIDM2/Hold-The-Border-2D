using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class SkillButton : MonoBehaviour
    {
        public event UnityAction<SkillData> SkillRequested;

        [SerializeField] private Image _icon;
        [SerializeField] private Image _cooldown;

        private Tween _cooldownTween;

        private Button _button;
        private TMP_Text _costText;

        private SkillData _data;


        private void Awake()
        {
            _button = GetComponent<Button>();
            _costText = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public void Init(SkillData data)
        {
            _data = data;

            _icon.sprite = data.Icon;
            _costText.text = data.Price.ToString();

            _cooldown.fillAmount = 1;
            _cooldownTween = _cooldown.DOFillAmount(0, _data.Cooldown).SetEase(Ease.Linear).SetAutoKill(false).Pause().OnComplete(EnableInteraction);
            _cooldown.fillAmount = 0;
        }

        public void EnableInteraction()
        {
            _button.interactable = true;
        }

        public void DisableInteraction()
        {
            _button.interactable = false;
        }

        public void ShowCooldown()
        {
            _cooldown.fillAmount = 1;
            _cooldownTween.Restart();
        }

        private void OnButtonClicked()
        {
            SkillRequested?.Invoke(_data);
        }
    }
}
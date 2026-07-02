using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event UnityAction<SkillData> SkillRequested;

        [SerializeField] private Image _icon;
        [SerializeField] private Image _cooldown;

        [SerializeField] private GameObject _tooltip;
        [SerializeField] private TMP_Text _tooltipText;

        private Tween _cooldownTween;

        private Button _button;
        private TMP_Text _costText;

        private SkillData _data;

        private bool _canAfford;
        private bool _isOnCooldown;

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
        public void OnPointerEnter(PointerEventData eventData)
        {
            _tooltip.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tooltip.SetActive(false);
        }

        public void Init(SkillData data)
        {
            _data = data;

            _icon.sprite = data.Icon;
            _costText.text = data.Price.ToString();

            _tooltip.SetActive(false);
            _tooltipText.text = _data.GetDescription();

            _cooldown.fillAmount = 1;
            _cooldownTween = _cooldown.DOFillAmount(0, _data.Cooldown).SetEase(Ease.Linear).SetAutoKill(false).Pause().OnComplete(OnCooldownCompllete);
            _cooldown.fillAmount = 0;

        }
        public void SetAffordable(bool affordable)
        {
            _canAfford = affordable;
            SetInteractable();
        }

        public void ShowCooldown()
        {
            _cooldown.fillAmount = 1;
            _isOnCooldown = true;
            _cooldownTween.Restart();
        }

        private void OnButtonClicked()
        {
            SkillRequested?.Invoke(_data);
        }

        private void OnCooldownCompllete()
        {
            _isOnCooldown = false;
            SetInteractable();
        }

        private void SetInteractable() => _button.interactable = _canAfford && !_isOnCooldown;
       
    }
}
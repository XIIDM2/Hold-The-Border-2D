using Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class SkillButton : MonoBehaviour
    {
        public event UnityAction<SkillData> SkillRequested;

        private Button _button;
        [SerializeField] private Image _icon;
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

        }

        private void OnButtonClicked()
        {
            SkillRequested?.Invoke(_data);
        }
    }
}
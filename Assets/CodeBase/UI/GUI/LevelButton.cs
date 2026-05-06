using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class LevelButton : MonoBehaviour
    {
        public event UnityAction<string, string> ButtonClicked;

        private Button _button;
        private TMP_Text _text;

        private string _levelName;
        private string _addressablesLabel;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public void Init(string levelName, string addressablesLabel)
        {
            _text.text = levelName;

            _levelName = levelName;
            _addressablesLabel = addressablesLabel;
        }

        public void OnButtonClicked()
        {
            ButtonClicked?.Invoke(_levelName, _addressablesLabel);
        }
    }
}
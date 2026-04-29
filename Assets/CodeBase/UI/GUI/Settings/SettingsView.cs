using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class SettingsView : MonoBehaviour
    {
        public event UnityAction DecreaseQualityRequested;
        public event UnityAction IncreaseQualityRequested;

        public event UnityAction<float> SFXVolumeChanged;
        public event UnityAction<float> AmbienceVolumeChanged;
        public event UnityAction<float> MusicVolumeChanged;

        public event UnityAction ExitSettingsRequested;

        [SerializeField] private Button _decreaseQualityButton;
        [SerializeField] private Button _increaseQualityButton;
        [SerializeField] private TMP_Text _currentQualityText;

        [SerializeField] private Slider _SFXSlider;
        [SerializeField] private Slider _ambiencelider;
        [SerializeField] private Slider _musicSlider;

        [SerializeField] private Button _exitButton;

        public void OnEnable()
        {
            _decreaseQualityButton.onClick.AddListener(OnDecreaseQualityButtonPresed);
            _increaseQualityButton.onClick.AddListener(OnIncreaseQualityButtonPresed);

            _SFXSlider.onValueChanged.AddListener(SFXSliderChanged);
            _ambiencelider.onValueChanged.AddListener(AmbienceSliderChanged);
            _musicSlider.onValueChanged.AddListener(MusicSliderChanged);

            _exitButton.onClick.AddListener(OnExitButtonPresed);
        }

        public void OnDisable()
        {
            _decreaseQualityButton.onClick.RemoveListener(OnDecreaseQualityButtonPresed);
            _increaseQualityButton.onClick.RemoveListener(OnIncreaseQualityButtonPresed);

            _SFXSlider.onValueChanged.RemoveListener(SFXSliderChanged);
            _ambiencelider.onValueChanged.RemoveListener(AmbienceSliderChanged);
            _musicSlider.onValueChanged.RemoveListener(MusicSliderChanged);

            _exitButton.onClick.RemoveListener(OnExitButtonPresed);
        }

        public void Init(string currentQualityText, float SFXValue, float AmbienceValue, float MusicValue)
        {
            gameObject.SetActive(false);

            _currentQualityText.text = currentQualityText;
            _SFXSlider.SetValueWithoutNotify(SFXValue);
            _ambiencelider.SetValueWithoutNotify(AmbienceValue);
            _musicSlider.SetValueWithoutNotify(MusicValue);
        }

        public void SetCurrentQualityText(string currentQualityText)
        {
            _currentQualityText.text = currentQualityText;
        }

        private void OnDecreaseQualityButtonPresed()
        {
            DecreaseQualityRequested?.Invoke();
        }

        private void OnIncreaseQualityButtonPresed()
        {
            IncreaseQualityRequested?.Invoke();
        }

        private void SFXSliderChanged(float value)
        {
            SFXVolumeChanged?.Invoke(value);
        }

        private void AmbienceSliderChanged(float value)
        {
            AmbienceVolumeChanged?.Invoke(value);
        }
        private void MusicSliderChanged(float value)
        {
            MusicVolumeChanged?.Invoke(value);
        }

        private void OnExitButtonPresed()
        {
            ExitSettingsRequested?.Invoke();
        }


    }
}
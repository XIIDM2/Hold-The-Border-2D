using TMPro;
using UnityEngine;
using VContainer;

public class WavesUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _wavesText;

    private IWaveService _waveService;

    [Inject]
    public void Construct(IWaveService waveService)
    {
        _waveService = waveService;
    }

    private void Start()
    {
        SetWave(_waveService.CurrentWaveIndex);
    }

    private void OnEnable()
    {
        _waveService.NextWaveStarted += SetWave;
    }

    private void OnDisable()
    {
        _waveService.NextWaveStarted -= SetWave;
    }

    private void SetWave(int currentWaveIndex)
    {
        _wavesText.text = $"{currentWaveIndex}/{_waveService.WavesLength}";
    }
}

using UnityEngine;
using VContainer;

public class GameManager : MonoBehaviour
{
    [Header("Services")]
    private IPlayerController _playerController;
    private IPathProvider _pathService;
    private IWaveService _waveManager;

    [Header("DATA")]
    private PlayerData _playerData;
    private WaveData _waveData;

    [Inject]
    public void Construct(IPlayerController playerController, IPathProvider pathService, IWaveService waveService, PlayerData playerData, WaveData waveData)
    {
        _playerController = playerController;
        _pathService = pathService;
        _waveManager = waveService;

        _playerData = playerData;
        _waveData = waveData;

    }

    private void Start()
    {
        _playerController.Init(_playerData);
        Messenger<int>.AddListener(Events.UnitReachedEnd, _playerController.Health.TakeDamage);

        StartCoroutine(_waveManager.WavesLogicRoutine(_waveData, _pathService));

    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(Events.UnitReachedEnd, _playerController.Health.TakeDamage);
    }

    private void Update()
    {
        Debug.Log("Current Player Health: " + _playerController.Health.CurrentHealth);
    }

}

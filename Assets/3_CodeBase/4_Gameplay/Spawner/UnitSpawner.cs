using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;

    private UnitFactory _factory;

    private void Awake()
    {
        _factory = new UnitFactory();
    }

    private void OnEnable()
    {
        Messenger<UnitType, Waypoint>.AddListener(Events.UnitSpawn, CreateUnit);
    }

    private void OnDisable()
    {
        Messenger<UnitType, Waypoint>.RemoveListener(Events.UnitSpawn, CreateUnit);
    }

    private void OnDestroy()
    {
        if (_factory != null) _factory.ReleaseUnitAssets();
    }

    private async void CreateUnit(UnitType type, Waypoint start)
    {
        await _factory.CreateUnit(type, start, _spawnPosition.position);
    }

}

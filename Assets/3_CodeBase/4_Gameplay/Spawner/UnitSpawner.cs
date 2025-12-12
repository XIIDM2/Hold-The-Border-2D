using UnityEngine;
using VContainer;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;

    private IUnitFactory _factory;

    [Inject]
    public void Construct(IUnitFactory factory)
    {
        _factory = factory;
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
       // if (_factory != null) _factory.ReleaseUnitAssets();
    }

    private async void CreateUnit(UnitType type, Waypoint start)
    {
        await _factory.CreateUnit(type, start, _spawnPosition.position);
    }

}

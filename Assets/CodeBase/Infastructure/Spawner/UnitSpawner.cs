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
        Messenger<UnitType, Waypoint>.AddListener(Events.UnitSpawned, CreateUnit);
    }

    private void OnDisable()
    {
        Messenger<UnitType, Waypoint>.RemoveListener(Events.UnitSpawned, CreateUnit);
    }

    private async void CreateUnit(UnitType type, Waypoint start)
    {
        await _factory.CreateUnit(type, start, _spawnPosition.position);
    }

}

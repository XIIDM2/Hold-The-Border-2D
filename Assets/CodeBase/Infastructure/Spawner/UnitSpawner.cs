using Gameplay.Units.Enemy;
using Infrastructure.Factories;
using UnityEngine;
using VContainer;

namespace Infrastructure.Spawners
{
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
            Messenger<EnemyUnitType, Waypoint>.AddListener(Events.UnitSpawned, CreateUnit);
        }

        private void OnDisable()
        {
            Messenger<EnemyUnitType, Waypoint>.RemoveListener(Events.UnitSpawned, CreateUnit);
        }

        private async void CreateUnit(EnemyUnitType type, Waypoint start)
        {
            await _factory.CreateUnit(type, start, _spawnPosition.position);
        }

    }
}
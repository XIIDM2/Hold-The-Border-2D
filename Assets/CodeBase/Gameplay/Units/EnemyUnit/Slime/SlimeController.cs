using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Factories;
using Infrastructure.Managers;
using UnityEngine;
using VContainer;

namespace Gameplay.Units.Enemy
{
    public class SlimeController : EnemyUnitController
    {
        [SerializeField] private Transform _firstSpawnPosition;
        [SerializeField] private Transform _secondSpawnPosition;

        private IUnitFactory _unitFactory;
        private GameplayRegistry _registry;
        private LevelManager _manager;

        [Inject]
        public void Construct(IUnitFactory unitFactory, GameplayRegistry registry,  LevelManager manager)
        {
            _unitFactory = unitFactory;
            _registry = registry;
            _manager = manager;

        }

        protected override void OnEnable()
        {
            base.OnEnable();

            Animation.DeathAnimationComplete += OnDeath;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Animation.DeathAnimationComplete -= OnDeath;
        }

        private void OnDeath()
        {
            _unitFactory.CreateUnit(EnemyUnitType.BabySlime, Pathing.CurrentWaypoint, _firstSpawnPosition.transform.position, _manager.GetCancellationTokenOnDestroy());
            _unitFactory.CreateUnit(EnemyUnitType.BabySlime, Pathing.CurrentWaypoint, _secondSpawnPosition.transform.position, _manager.GetCancellationTokenOnDestroy());
        }
    }
}
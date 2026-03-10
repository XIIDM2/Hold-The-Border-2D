using Cysharp.Threading.Tasks;
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

        private ILevelManager _manager;

        [Inject]
        public void Construct(IUnitFactory unitFactory,  ILevelManager manager)
        {
            _unitFactory = unitFactory;
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
            _unitFactory.CreateUnit(EnemyUnitType.BabySlime, Pathing.CurrentWaypoint, _firstSpawnPosition.transform.position, _manager.GameObject.GetCancellationTokenOnDestroy());
            _unitFactory.CreateUnit(EnemyUnitType.BabySlime, Pathing.CurrentWaypoint, _secondSpawnPosition.transform.position, _manager.GameObject.GetCancellationTokenOnDestroy());
        }
    }
}
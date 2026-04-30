using Infrastructure.Factories;
using System.Threading;
using UnityEngine;
using VContainer;

namespace Gameplay.Units.Enemy
{
    public class SlimeController : EnemyUnitController
    {
        [SerializeField] private Transform _firstSpawnPosition;
        [SerializeField] private Transform _secondSpawnPosition;

        private IUnitFactory _unitFactory;
        private CancellationToken _ctc;

        [Inject]
        public void Construct(IUnitFactory unitFactory, CancellationToken ctc)
        {
            _unitFactory = unitFactory;
            _ctc = ctc;
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
            _unitFactory.CreateUnit(EnemyUnitType.BabySlime, Pathing.CurrentWaypoint, _firstSpawnPosition.transform.position, _ctc);
            _unitFactory.CreateUnit(EnemyUnitType.BabySlime, Pathing.CurrentWaypoint, _secondSpawnPosition.transform.position, _ctc);
        }
    }
}
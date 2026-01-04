using Core.FSM;
using Core.Utilities.CustomProperties;
using Data;
using Gameplay.Units.FSM.Enemy;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Gameplay.Units.Enemy
{
    [RequireComponent(typeof(UnitMovement))]
    [RequireComponent(typeof(EnemyUnitPathing))]
    public class EnemyUnitController : BaseUnitController<EnemyUnitController>, ITargetable
    {
        public Vector2 Position => transform.position;
        public int PathEndDamage { get; private set; }

        [SerializeField, ReadOnly] private int _currentHealth;

        [Header("FSM")]
        public EnemyUnitMoveState MoveState { get; private set; }

        private void Start()
        {
            MoveState = new EnemyUnitMoveState();

            ActionFSM = new FiniteStateMachine<EnemyUnitController>();
            ActionFSM.StateInit(MoveState, this);
        }

        private void OnEnable()
        {
            Damageable.OnDeath += DestroyUnit;
        }

        private void OnDisable()
        {
            Damageable.OnDeath -= DestroyUnit;
        }

        protected override void Update()
        {
            base.Update();

            _currentHealth = Damageable.CurrentHealth;
        }

        public void Init(EnemyUnitData data, Waypoint start)
        {
            PathEndDamage = data.PathEndDamage;

            Damageable?.Init(data.MaxHealth);

            if (Animation) Animation.Init(data.Animations);
            if (Movement) Movement.Init(data.MovementSpeed);
            if (Attack) Attack.Init(data.AttackDamage, data.AttackCooldown);
            if (Pathing) Pathing.Init(start);
        }

        public void DestroyUnit(IDamageable damageable)
        {
            Destroy(gameObject);
        }

    }
}
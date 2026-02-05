using Core.FSM;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Path;
using Gameplay.Player;
using Gameplay.Units.Enemy.FSM;
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using UnityEngine;
using VContainer;

namespace Gameplay.Units.Enemy
{
    [RequireComponent(typeof(UnitMovement))]
    [RequireComponent(typeof(EnemyUnitPathing))]
    public class EnemyUnitController : BaseUnitController<EnemyUnitController>, ITargetable
    {
        public Vector2 Position => transform.position;
        public int PathEndDamage { get; private set; }

        [Header("Dependencies")]
        public IPlayerController Player {  get; private set; }
        private IUIFactory _UIFactory;

        [Header("FSM")]
        public EnemyUnitMoveState MoveState { get; private set; }

        private int _currentHealth; // For DamagePopup

        [Inject]
        public void Construct(IPlayerController player, IUIFactory UIFactory)
        {
            Player = player;
            _UIFactory = UIFactory;
        }

        private void Start()
        {
            MoveState = new EnemyUnitMoveState();

            ActionFSM = new FiniteStateMachine<EnemyUnitController>();
            ActionFSM.StateInit(MoveState, this);

            _currentHealth = Health.CurrentHealth;
        }

        private void OnEnable()
        {
            Health.Death += DestroyUnit;
            Health.HealthChanged += OnDamageRecieved;
        }

        private void OnDisable()
        {
            Health.Death -= DestroyUnit;
            Health.HealthChanged -= OnDamageRecieved;
        }

        public void Initialize(EnemyUnitData data, Waypoint start)
        {
            PathEndDamage = data.PathEndDamage;

            Health?.Initialize(data.MaxHealth);

            if (Pathing) Pathing.Initialize(start);
            if (Movement) Movement.Initialize(data.MovementSpeed);
            if (Attack) Attack.Initialize(data.AttackDamage, data.AttackCooldown);
            if (Animation) Animation.Initialize(data.OverrideAnimations);
        }

        private void OnDamageRecieved(int healthAfterDamage)
        {
            _UIFactory.CreateDamagePopup(gameObject.transform.position, _currentHealth - healthAfterDamage).Forget();
            _currentHealth = healthAfterDamage;
        }

        public void DestroyUnit(IDamageable damageable)
        {
            Destroy(gameObject);
        }

    }
}
using Core.FSM;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Units.FSM.Enemy;
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

        private int _currentHealth;

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

            _currentHealth = Damageable.CurrentHealth;
        }

        private void OnEnable()
        {
            Damageable.OnDeath += DestroyUnit;
            Damageable.OnHealthChanged += OnDamageRecieved;
        }

        private void OnDisable()
        {
            Damageable.OnDeath -= DestroyUnit;
            Damageable.OnHealthChanged -= OnDamageRecieved;
        }

        public void Init(EnemyUnitData data, Waypoint start)
        {
            PathEndDamage = data.PathEndDamage;

            Damageable?.Init(data.MaxHealth);

            if (Animation) Animation.Init(data.OverrideAnimations);
            if (Movement) Movement.Init(data.MovementSpeed);
            if (Attack) Attack.Init(data.AttackDamage, data.AttackCooldown);
            if (Pathing) Pathing.Init(start);
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
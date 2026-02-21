using Core.FSM;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Path;
using Gameplay.Player;
using Gameplay.UI;
using Gameplay.Units.Enemy.FSM;
using Gameplay.Units.FSM;
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
        public EnemyUnitType Type {  get; private set; }
        public Vector2 Position => transform.position;
        public int PathEndDamage { get; private set; }

        [Header("Dependencies")]
        public IPlayerController Player {  get; private set; }
        public UnitFactory UnitFactory { get; private set; }

        private IUIFactory _UIFactory;

        private HealthBarUI _healthUI;

        [Header("FSM")]
        public EnemyUnitMoveState MoveState { get; private set; }
        public EnemyUnitDeathState DeathState { get; private set; }
        public EnemyUnitDissolveState DissolveState { get; private set; }

        private int _currentHealth; // For DamagePopup

        [Inject]
        public void Construct(IPlayerController player, IUIFactory UIFactory)
        {
            Player = player;
            _UIFactory = UIFactory;
        }

        protected override void Awake()
        {
            base.Awake();

            _healthUI = GetComponentInChildren<HealthBarUI>();
        }


        private void Start()
        {
            MoveState = new EnemyUnitMoveState();

            DeathState = new EnemyUnitDeathState();

            DissolveState = new EnemyUnitDissolveState();

            ActionFSM = new FiniteStateMachine<EnemyUnitController>();
            ActionFSM.StateInit(MoveState, this);
        }

        protected virtual void OnEnable()
        {
            Health.HealthChanged += OnDamageRecieved;
        }

        protected virtual void OnDisable()
        {
            Health.HealthChanged -= OnDamageRecieved;
        }

        public void Init(EnemyUnitData data, Waypoint start)
        {

            PathEndDamage = data.PathEndDamage;

            Health?.Init(data.MaxHealth);
            _currentHealth = Health.CurrentHealth;

            if (Pathing) Pathing.Init(start);
            if (Movement) Movement.Init(data.MovementSpeed);
            if (Attack) Attack.Init(data.AttackDamage, data.AttackCooldown);
            if (Animation) Animation.Init(data.OverrideAnimations);

            if(_healthUI) _healthUI.Init();

            ActionFSM?.ChangeState(MoveState, this);
        }

        public void InitPool(EnemyUnitType type, UnitFactory unitFactory)
        {
            Type = type;
            UnitFactory = unitFactory;
        }

        private void OnDamageRecieved(int healthAfterDamage)
        {
            _UIFactory.CreateDamagePopup(gameObject.transform.position, _currentHealth - healthAfterDamage, this.GetCancellationTokenOnDestroy()).Forget();
            _currentHealth = healthAfterDamage;
        }

        public void DestroyUnit()
        {
            Destroy(gameObject);
        }


    }
}
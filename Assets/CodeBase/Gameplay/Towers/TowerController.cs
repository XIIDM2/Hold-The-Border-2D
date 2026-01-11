using Core.FSM;
using Core.Interfaces;
using Data;
using Gameplay.Towers.FSM;
using Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Towers
{
    public class TowerController : MonoBehaviour, IControllable
    {
        public event UnityAction UpgradeRequested;
        public int CurrentTierIndex { get; private set; }
        public int MaxTier => _data.TierConfigs.Length - 1;
        public TowerData.TowerTierConfig CurrentTierConfig => _data.TierConfigs[CurrentTierIndex];

        [SerializeField] private TowerData _data;

        [Header("Components")]
        public TowerDetection Detection { get; private set; }
        public BaseTowerAttack Attack { get; private set; }
        public TowerAnimation Animation { get; private set; }

        [Header("FSM")]
        public FiniteStateMachine<TowerController> ActionFSM { get; private set; }
        public TowerUpgradeState UpgradeState { get; private set; }
        public TowerIdleState IdleState { get; private set; }
        public TowerAttackState AttackState { get; private set; }

        private void Awake()
        {
            Detection = GetComponent<TowerDetection>();
            Attack = GetComponent<BaseTowerAttack>();

            Animation = GetComponentInChildren<TowerAnimation>();
        }

        private void Start()
        {
            Init();

            UpgradeState = new TowerUpgradeState();
            IdleState = new TowerIdleState();
            AttackState = new TowerAttackState();

            ActionFSM = new FiniteStateMachine<TowerController>();
            ActionFSM.StateInit(IdleState, this);
        }

        private void OnEnable()
        {
            Detection.TargetEntered += Attack.AddToAttackList;
            Detection.TargetExited += Attack.RemoveFromAttackList;
        }

        private void OnDisable()
        {
            Detection.TargetEntered -= Attack.AddToAttackList;
            Detection.TargetExited -= Attack.RemoveFromAttackList;
        }

        private void Update()
        {
            ActionFSM.UpdateState(this);
        }

        private void LateUpdate()
        {
            ActionFSM.LateUpdateState(this);

        }

        private void FixedUpdate()
        {
            ActionFSM.FixedUpdateState(this);
        }

        public void Init()
        {
            ApplyCurrentTier();
            // первичная инициализация
        }

        public void ApplyCurrentTier()
        {
            Animation.Init(CurrentTierConfig.UpgradeAnimation, CurrentTierConfig.IdleAnimation);
            Detection.Init(CurrentTierConfig.AttackRadius);

            if (Attack) Attack.Init(CurrentTierConfig.Damage, CurrentTierConfig.AttackCooldown);

            if (Attack is IProjectileRequireable projectileRequireable)
            {
                projectileRequireable.InitProjectile(CurrentTierConfig.ProjectilePrefab);
            }

            // назначение данных
        }

        public void MoveToNextTier()
        {
            if (CurrentTierIndex >= MaxTier) return;

            CurrentTierIndex++;
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 50), "Upgrade Tower"))
            {
                UpgradeRequested?.Invoke();
            }
        }

    }
}
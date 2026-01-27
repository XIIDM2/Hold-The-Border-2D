using Core.FSM;
using Core.Interfaces;
using Data;
using Gameplay.Towers.FSM;
using Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VContainer;

namespace Gameplay.Towers
{
    public class TowerController : MonoBehaviour, IControllable, IPointerClickHandler
    {
        public UnityAction UpgradeRequested;
        public int CurrentTierIndex { get; private set; }
        public int MaxTier => _data.TierConfigs.Length - 1;
        public TowerData.TowerTierConfig currentTierConfig => _data.TierConfigs[CurrentTierIndex];


        [Header("Components")]
        public TowerDetection Detection { get; private set; }
        public BaseTowerAttack Attack { get; private set; }
        public TowerAnimation Animation { get; private set; }

        private ITowerSelectionService _selectionService;

        [Header("FSM")]
        public FiniteStateMachine<TowerController> ActionFSM { get; private set; }
        public TowerUpgradeState UpgradeState { get; private set; }
        public TowerIdleState IdleState { get; private set; }
        public TowerAttackState AttackState { get; private set; }

        private TowerData _data;

        [Inject]
        public void Construct(ITowerSelectionService selectionService)
        {
            _selectionService = selectionService;
        }

        private void Awake()
        {
            Detection = GetComponent<TowerDetection>();
            Attack = GetComponent<BaseTowerAttack>();

            Animation = GetComponentInChildren<TowerAnimation>();
        }

        private void Start()
        {
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

        public void OnPointerClick(PointerEventData eventData)
        {
            _selectionService.SelectTower(this);
        }

        public void Init(TowerData data)
        {
            _data = data;

            ApplyCurrentTier();
            // первичная инициализация
        }

        public void ApplyCurrentTier()
        {
            Animation.Init(currentTierConfig.UpgradeAnimation, currentTierConfig.IdleAnimation);
            Detection.Init(currentTierConfig.AttackRadius);

            if (Attack is TowerAttackByUnits attackByUnits)
            {
                attackByUnits.InitTowerUnitVisualPrefab(currentTierConfig.TowerVisualPrefab);
            }

            if (Attack) Attack.Init(currentTierConfig.Damage, currentTierConfig.AttackCooldown);

            if (Attack is IProjectileRequireable projectileRequireable)
            {
                projectileRequireable.InitProjectile(currentTierConfig.ProjectilePrefab);
            }

            // назначение данных
        }

        public void MoveToNextTier()
        {
            if (CurrentTierIndex >= MaxTier) return;

            CurrentTierIndex++;
        }


    }
}
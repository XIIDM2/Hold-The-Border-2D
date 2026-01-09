using Core.FSM;
using Data;
using Gameplay.Towers.Units.FSM;
using Gameplay.Units;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Gameplay.Towers.Units
{
    public class RangedUnitController : BaseUnitController<RangedUnitController>, IAttacker
    {
        public Vector2 TargetDirection { get; private set; }
        public ObjectDirection Direction { get; private set; }

        [SerializeField] private AnimationData _data;


        [Header("FSM")]
        public RangedUnitIdleState IdleState { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Direction = new ObjectDirection();
        }

        private void Start()
        {
            IdleState = new RangedUnitIdleState();

            ActionFSM = new FiniteStateMachine<RangedUnitController>();
            ActionFSM.StateInit(IdleState, this);
        }

        public void Init(int damage, float coolDown)
        {
            Animation.Init(_data);

            if (Attack) Attack.Init(damage, coolDown);
        }

        public void ExecuteAttack(ITargetable target)
        {
            if (target is Component component)
            {
                TargetDirection = component.gameObject.transform.position - transform.position;
            }

            Attack.Attack(target);
        }
    }
}
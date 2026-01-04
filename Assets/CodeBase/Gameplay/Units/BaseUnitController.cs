using Core.Interfaces;
using Core.FSM;
using Infrastructure.Interfaces;
using UnityEngine;
using Gameplay.Units.Enemy;

namespace Gameplay.Units
{
    public abstract class BaseUnitController<T> : MonoBehaviour, IControllable where T : BaseUnitController<T>
    {
        [Header("Components")]
        public IDamageable Damageable { get; protected set; }
        public UnitMovement Movement { get; protected set; }
        public EnemyUnitPathing Pathing { get; protected set; }
        public BaseUnitAttack Attack { get; protected set; }
        public UnitAnimation Animation { get; protected set; }

        [Header("FSM")]
        public FiniteStateMachine<T> ActionFSM { get; protected set; }

        protected virtual void Awake()
        {
            Damageable = new Health();
            Attack = GetComponent<EnemyUnitAttack>();
            Movement = GetComponent<UnitMovement>();
            Pathing = GetComponent<EnemyUnitPathing>();
            Animation = GetComponentInChildren<UnitAnimation>();
        }

        protected virtual void Update()
        {
            ActionFSM.UpdateState((T)this);
        }

        protected virtual void LateUpdate()
        {
            ActionFSM.LateUpdateState((T)this);

        }

        protected virtual void FixedUpdate()
        {
            ActionFSM.FixedUpdateState((T)this);
        }
    }
}
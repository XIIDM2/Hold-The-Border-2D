using Core.FSM;
using Core.Interfaces;
using Gameplay.Units.Enemy;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Gameplay.Units
{
    public abstract class BaseUnitController<T> : MonoBehaviour, IControllable where T : BaseUnitController<T>
    {
        [Header("Components")]
        public IDamageable Health { get; protected set; }
        public UnitMovement Movement { get; protected set; }
        public EnemyUnitPathing Pathing { get; protected set; }
        public BaseUnitAttack Attack { get; protected set; }
        public UnitAnimation Animation { get; protected set; }

        [Header("FSM")]
        public FiniteStateMachine<T> ActionFSM { get; protected set; }

        protected virtual void Awake()
        {
            Health = new Health();
            Attack = GetComponent<BaseUnitAttack>();
            Movement = GetComponent<UnitMovement>();
            Pathing = GetComponent<EnemyUnitPathing>();
            Animation = GetComponentInChildren<UnitAnimation>();
        }

        protected virtual void Update()
        {
            ActionFSM.Update((T)this);
        }

        protected virtual void LateUpdate()
        {
            ActionFSM.LateUpdate((T)this);

        }

        protected virtual void FixedUpdate()
        {
            ActionFSM.FixedUpdate((T)this);
        }
    }
}
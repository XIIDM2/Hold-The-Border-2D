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
        [Header("FSM")]
        public RangedUnitIdleState IdleState { get; private set; }

        private void Start()
        {
            IdleState = new RangedUnitIdleState();

            ActionFSM = new FiniteStateMachine<RangedUnitController>();
            ActionFSM.StateInit(IdleState, this);
        }

        public void Init(AnimationData animations, int damage, float coolDown)
        {
            if (Animation) Animation.Init(animations);
            if (Attack) Attack.Init(damage, coolDown);
        }

        public void ExecuteAttack()
        {
            Debug.Log("Attacking!");
        }
    }
}
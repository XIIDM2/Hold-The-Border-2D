using UnityEngine;

namespace Gameplay.Towers.Units.FSM
{
    public class RangedUnitIdleState : RangedUnitState
    {
        public override void Update(RangedUnitController controller)
        {
            base.Update(controller);

            Debug.Log("Im in idle state");
        }
    }
}
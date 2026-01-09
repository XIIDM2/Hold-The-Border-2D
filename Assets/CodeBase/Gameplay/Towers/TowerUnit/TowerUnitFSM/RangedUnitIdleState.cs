using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Towers.Units.FSM
{
    public class RangedUnitIdleState : RangedUnitState
    {
        public override void Update(RangedUnitController controller)
        {
            base.Update(controller);

            controller.Direction.FaceDirection(controller.transform, controller.TargetDirection.x);
            controller.Animation.SetFloat(UnitAnimationParameter.Horizontal.ToString(), controller.TargetDirection.x);
            controller.Animation.SetFloat(UnitAnimationParameter.Vertical.ToString(), controller.TargetDirection.y);      
        }
    }
}
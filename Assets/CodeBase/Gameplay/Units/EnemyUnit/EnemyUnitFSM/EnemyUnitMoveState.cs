using Core.FSM;
using Gameplay.Units.Enemy;

namespace Gameplay.Units.FSM.Enemy
{
    public class EnemyUnitMoveState : UnitState<EnemyUnitController>
    {
        public override State<EnemyUnitController> HandleTransitions(EnemyUnitController controller)
        {
            return base.HandleTransitions(controller);
        }

        public override void Enter(EnemyUnitController controller)
        {
            base.Enter(controller);
            controller.Animation.SetBool(UnitAnimationParameter.IsMoving.ToString(), true);
        }

        public override void Update(EnemyUnitController controller)
        {
            base.Update(controller);

            controller.Animation.SetFloat(UnitAnimationParameter.Horizontal.ToString(), controller.Movement.Direction.x);
            controller.Animation.SetFloat(UnitAnimationParameter.Vertical.ToString(), controller.Movement.Direction.y);

            if (controller.Pathing.CheckWaypointReached())
            {
                controller.Pathing.SetNextWaypoint();

                if (controller.Pathing.HasReachedEnd())
                {
                    controller.Player.Health.TakeDamage(controller.PathEndDamage);
                    controller.DestroyUnit(controller.Damageable);
                    return;
                }

            }
        }

        public override void FixedUpdate(EnemyUnitController controller)
        {
            base.FixedUpdate(controller);

            controller.Movement.Move(controller.Pathing.CurrentPosition);
        }

        public override void Exit(EnemyUnitController controller)
        {
            base.Exit(controller);

            controller.Animation.SetBool(UnitAnimationParameter.IsMoving.ToString(), false);
        }
    }
}
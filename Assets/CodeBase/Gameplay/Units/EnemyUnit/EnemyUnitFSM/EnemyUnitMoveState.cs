using Gameplay.Units.FSM;

namespace Gameplay.Units.Enemy.FSM
{
    public class EnemyUnitMoveState : UnitState<EnemyUnitController>
    {
        public override void Enter(EnemyUnitController controller)
        {
            base.Enter(controller);
            controller.Animation.SetBool(controller.Animation.IsMovingHash, true);
        }

        public override void Update(EnemyUnitController controller)
        {
            base.Update(controller);

            controller.Animation.SetFloat(controller.Animation.HorizontalHash, controller.Movement.Direction.x);
            controller.Animation.SetFloat(controller.Animation.VerticalHash, controller.Movement.Direction.y);

            if (controller.Pathing.CheckWaypointReached())
            {
                controller.Pathing.SetNextWaypoint();

                if (controller.Pathing.HasReachedEnd())
                {
                    controller.Player.Health.TakeDamage(controller.PathEndDamage);
                    controller.DestroyUnit(controller.Health);
                    return;
                }

            }
        }

        public override void FixedUpdate(EnemyUnitController controller)
        {
            base.FixedUpdate(controller);

            controller.Movement.Move(controller.Pathing.WaypointPosition);
        }

        public override void Exit(EnemyUnitController controller)
        {
            base.Exit(controller);

            controller.Animation.SetBool(controller.Animation.IsMovingHash, false);
        }
    }
}
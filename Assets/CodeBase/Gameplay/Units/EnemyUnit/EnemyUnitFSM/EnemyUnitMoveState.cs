using Core.FSM;
using Gameplay.Units.FSM;
using Infrastructure.Interfaces;

namespace Gameplay.Units.Enemy.FSM
{
    public class EnemyUnitMoveState : UnitState<EnemyUnitController>
    {
        private bool isDead = false;

        public override State<EnemyUnitController> HandleTransitions(EnemyUnitController controller)
        {
            if (isDead) return controller.DeathState;

            return base.HandleTransitions(controller);
        }

        public override void Enter(EnemyUnitController controller)
        {
            base.Enter(controller);

            controller.Health.Death += OnDeath;

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
                    controller.DestroyUnit();
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

            controller.Health.Death -= OnDeath;

            controller.Animation.SetBool(controller.Animation.IsMovingHash, false);
        }

        private void OnDeath(IDamageable damageable)
        {
            isDead = true;
        }
    }
}
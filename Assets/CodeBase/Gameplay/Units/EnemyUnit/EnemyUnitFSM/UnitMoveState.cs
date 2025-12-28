using UnityEngine;

public class UnitMoveState : UnitState
{
    public override State<EnemyUnitController> HandleTransitions(EnemyUnitController controller)
    {
        return base.HandleTransitions(controller);
    }

    public override void Enter(EnemyUnitController controller)
    {
        base.Enter(controller);
        controller.Animation.SetBool(EnemyUnitAnimationParameter.IsMoving.ToString(), true);
    }

    public override void Update(EnemyUnitController controller)
    {
        base.Update(controller);

        controller.Animation.SetFloat(EnemyUnitAnimationParameter.Horizontal.ToString(), controller.Movement.Direction.x);
        controller.Animation.SetFloat(EnemyUnitAnimationParameter.Vertical.ToString(), controller.Movement.Direction.y);

        if (controller.Pathing.CheckWaypointReached())
        { 
            controller.Pathing.SetNextWaypoint();

            if (controller.Pathing.HasReachedEnd())
            {
                Messenger<int>.Broadcast(Events.UnitReachedEnd, controller.PathEndDamage);
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

        controller.Animation.SetBool(EnemyUnitAnimationParameter.IsMoving.ToString(), false);
    }
}

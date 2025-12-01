using UnityEngine;

public class UnitMoveState : UnitState
{
    public override State<UnitController> HandleTransitions(UnitController controller)
    {
        return base.HandleTransitions(controller);
    }

    public override void Enter(UnitController controller)
    {
        base.Enter(controller);
    }

    public override void Update(UnitController controller)
    {
        base.Update(controller);

        controller.Animation.SetFloat(UnitAnimationParameter.Horizontal, controller.Movement.Direction.x);
        controller.Animation.SetFloat(UnitAnimationParameter.Vertical, controller.Movement.Direction.y);
        controller.Animation.SetBool(UnitAnimationParameter.IsMoving, true);
    }

    public override void FixedUpdate(UnitController controller)
    {
        base.FixedUpdate(controller);

        controller.Movement.Move(controller._testTransform.position);
    }

    public override void Exit(UnitController controller)
    {
        base.Exit(controller);

        controller.Animation.SetBool(UnitAnimationParameter.IsMoving, false);
    }
}

using UnityEngine;

public class TowerIdleState : TowerState
{
    public override State<TowerController> HandleTransitions(TowerController controller)
    { 
        if (controller.Attack.UnitsToAttack.Count > 0)
        {
            return controller.AttackState;
        }

        return base.HandleTransitions(controller);
    }
}

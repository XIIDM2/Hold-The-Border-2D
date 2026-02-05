using Core.FSM;

namespace Gameplay.Towers.FSM
{
    public class TowerIdleState : TowerState
    {
        public override State<TowerController> HandleTransitions(TowerController controller)
        {
            if (controller.Attack.UnitsInRange.Count > 0)
            {
                return controller.AttackState;
            }

            return base.HandleTransitions(controller);
        }
    }
}
using Core.FSM;

namespace Gameplay.Towers.FSM
{
    public class TowerAttackState : TowerState
    {
        public override State<TowerController> HandleTransitions(TowerController controller)
        {
            if (controller.Attack.UnitsInRange.Count <= 0)
            {
                return controller.IdleState;
            }

            return base.HandleTransitions(controller);
        }
        public override void Enter(TowerController controller)
        {
            base.Enter(controller);

            controller.Attack.Attack();
        }

        public override void Exit(TowerController controller)
        {
            base.Exit(controller);

            controller.Attack.StopAttack();
        }
    }
}
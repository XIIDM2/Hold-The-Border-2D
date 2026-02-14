using Core.FSM;
using Gameplay.Units.Enemy;


namespace Gameplay.Units.FSM
{
    public class EnemyUnitDeathState : UnitState<EnemyUnitController>
    {
        private bool _isAnimationComplete = false;
        public override State<EnemyUnitController> HandleTransitions(EnemyUnitController controller)
        {
            if (_isAnimationComplete)
            {
                if (controller.Animation.CanDissolve)
                {
                    return controller.DissolveState;
                }
                else
                {
                    controller.DestroyUnit();
                }
            }

            return base.HandleTransitions(controller);
        }

        public override void Enter(EnemyUnitController controller)
        {
            controller.Animation.DeathAnimationComplete += OnAnimationComplete;

            controller.Pathing.enabled = false;
            controller.Attack.enabled = false;
            controller.Movement.enabled = false;

            controller.Animation.SetBool(controller.Animation.IsDeadHash, true);
        }

        public override void Exit(EnemyUnitController controller)
        {

            controller.Animation.DeathAnimationComplete -= OnAnimationComplete;

            controller.Animation.SetBool(controller.Animation.IsDeadHash, false);
        }

        private void OnAnimationComplete()
        {
            _isAnimationComplete = true;
        }
       
    }
}
using Core.FSM;
using Cysharp.Threading.Tasks;
using Gameplay.Units.Enemy;
using System;


namespace Gameplay.Units.FSM
{
    public class EnemyUnitDissolveState : UnitState<EnemyUnitController>
    {
        private const float TIME_BEFORE_DISSOLVE = 2.0f;
        private bool _isAnimationComplete = false;

        public override State<EnemyUnitController> HandleTransitions(EnemyUnitController controller)
        {
            if (_isAnimationComplete) controller.UnitFactory.ReturnToPool(controller.Type, controller);

            return base.HandleTransitions(controller);
        }

        public override void Enter(EnemyUnitController controller)
        {
            base.Enter(controller);

            _isAnimationComplete = false;

            controller.Pathing.enabled = true;
            controller.Attack.enabled = true;
            controller.Movement.enabled = true;

            controller.Animation.DissolveAnimationComplete += OnAnimationComplete;

            WaitBeforeDissolve(controller).Forget();
        }

        public override void Exit(EnemyUnitController controller)
        {
            base.Exit(controller);


            controller.Animation.DissolveAnimationComplete -= OnAnimationComplete;

            controller.Animation.SetBool(controller.Animation.IsDissolvingHash, false);
        }

        private async UniTaskVoid WaitBeforeDissolve(EnemyUnitController controller)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(TIME_BEFORE_DISSOLVE));

            controller.Animation.SetBool(controller.Animation.IsDissolvingHash, true);
        }

        private void OnAnimationComplete()
        {
            _isAnimationComplete = true;
        }

    }
}
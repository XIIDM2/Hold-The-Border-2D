using UnityEngine;

namespace Gameplay.Towers.SMB
{
    public class TowerUpgradeBehaviour : StateMachineBehaviour
    {
        private TowerAnimation _animation;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _animation ??= animator.GetComponent<TowerAnimation>();
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _animation?.OnUpgradeAnimationComplete();
        }
    }
}
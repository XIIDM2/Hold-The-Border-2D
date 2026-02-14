using UnityEngine;

namespace Gameplay.Units.SMB
{
    public class UnitDissolveBehaviour : StateMachineBehaviour
    {
        private UnitAnimation _animation;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _animation = _animation == null ? animator.GetComponent<UnitAnimation>() : _animation;
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime > 1.0f) _animation.NotifyDissolveAnimationComplete();
        }
    }
}
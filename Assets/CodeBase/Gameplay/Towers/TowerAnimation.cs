using Unity.VisualScripting;
using UnityEngine;

public class TowerAnimation : MonoBehaviour
{
    private const string UPGRADE_DEFAULT_CLIP_NAME = "Tower_Upgrade";
    private const string IDLE_DEFAULT_CLIP_NAME = "Tower_Idle";

    private const string UPGRADE_STATE_NAME = "Upgrade";

    private Animator _animator;
    private AnimatorOverrideController _overrideController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _overrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);

        _animator.runtimeAnimatorController = _overrideController;
    }

    public void UpgradeTowerAnimations(AnimationClip upgradeClip, AnimationClip idleClip)
    {
        _overrideController[UPGRADE_DEFAULT_CLIP_NAME] = upgradeClip;
        _overrideController[IDLE_DEFAULT_CLIP_NAME] = idleClip;
    }

    public void PlayUpgradeAnimation()
    {
        _animator.Play(UPGRADE_STATE_NAME);
    }
}

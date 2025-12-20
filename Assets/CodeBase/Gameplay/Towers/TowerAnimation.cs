using UnityEngine;

public class TowerAnimation : MonoBehaviour
{
    private const string UPGRADE_DEFAULT_CLIP_NAME = "Tower_Upgrade";
    private const string IDLE_DEFAULT_CLIP_NAME = "Tower_Idle";

    private const string UPGRADE_TRIGGER_PARAMETER_NAME = "Upgrade";

    private Animator _animator;
    private AnimatorOverrideController overrideController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        overrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);

        _animator.runtimeAnimatorController = overrideController;
    }

    public void PlayUpgradeAnimation(AnimationClip upgradeClip)
    {
        overrideController[UPGRADE_DEFAULT_CLIP_NAME] = upgradeClip;

        _animator.Play(UPGRADE_TRIGGER_PARAMETER_NAME);
    }

    public void ChangeIdleAnimation(AnimationClip idleClip)
    {
        overrideController[IDLE_DEFAULT_CLIP_NAME] = idleClip;
    }
}

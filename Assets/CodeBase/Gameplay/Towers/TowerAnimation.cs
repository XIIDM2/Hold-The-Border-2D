using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class TowerAnimation : MonoBehaviour
{
    public event UnityAction UpgradeAnimationCompleted;

    private const string UPGRADE_DEFAULT_CLIP_NAME = "Tower_Upgrade";
    private const string IDLE_DEFAULT_CLIP_NAME = "Tower_Idle";

    private static readonly int _upgradeStateHash = Animator.StringToHash("Upgrade");

    private Animator _animator;
    private AnimatorOverrideController _overrideController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _overrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);

        _animator.runtimeAnimatorController = _overrideController;
    }

    public void ApplyCurrentTier(AnimationClip upgradeClip, AnimationClip idleClip)
    {
        _overrideController[UPGRADE_DEFAULT_CLIP_NAME] = upgradeClip;
        _overrideController[IDLE_DEFAULT_CLIP_NAME] = idleClip;
    }

    public void PlayUpgradeAnimation()
    {
        _animator.Play(_upgradeStateHash);
    }

    public void OnUpgradeAnimationComplete()
    {
        UpgradeAnimationCompleted?.Invoke();
    }
}

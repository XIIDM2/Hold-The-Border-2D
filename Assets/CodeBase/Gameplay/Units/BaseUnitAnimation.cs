using UnityEngine;

public abstract class BaseUnitAnimation<T> : MonoBehaviour where T : BaseUnitAnimationData
{
    protected Animator _animator;
    protected AnimatorOverrideController _animatorOverrideController;

    protected AnimationClipOverrides _clipOverrides;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverrideController;

        _clipOverrides = new AnimationClipOverrides(_animatorOverrideController.overridesCount);
        _animatorOverrideController.GetOverrides(_clipOverrides);
    }


    public abstract void Init(T data);

    protected void ApplyClipsOverrides()
    {
        _animatorOverrideController.ApplyOverrides(_clipOverrides);
    }

    public void SetFloat(string parameterName, float value)
    {
        _animator.SetFloat(parameterName, value);

    }

    public void SetBool(string parameterName, bool value)
    {
        _animator.SetBool(parameterName, value);
    }

    public void SetTrigger(string parameterName)
    {
        _animator.SetTrigger(parameterName);
    }

}

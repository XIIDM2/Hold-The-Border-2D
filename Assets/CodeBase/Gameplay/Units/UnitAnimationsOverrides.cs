using UnityEngine;

public abstract class UnitAnimationsOverrides<T> : BaseUnitAnimation where T : BaseUnitAnimationData
{
    protected AnimatorOverrideController _animatorOverrideController;

    protected AnimationClipOverrides _clipOverrides;

    protected override void Awake()
    {
        base.Awake();

        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverrideController;

        _clipOverrides = new AnimationClipOverrides(_animatorOverrideController.overridesCount);
        _animatorOverrideController.GetOverrides(_clipOverrides);
    }

    public override void Init(BaseUnitAnimationData data)
    {
        if (data is T specificData)
        {
            InitData(specificData);
        }
        else
        {
            Debug.LogWarning("Wrong data type for Unit animation override for " + gameObject.name);
        }
       
    }

    protected abstract void InitData(T data);

    protected void ApplyClipsOverrides()
    {
        _animatorOverrideController.ApplyOverrides(_clipOverrides);
    }


}
    
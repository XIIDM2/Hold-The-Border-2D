using UnityEngine;

public abstract class BaseUnitAnimation : MonoBehaviour
{
    protected Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public abstract void Init(BaseUnitAnimationData data);

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
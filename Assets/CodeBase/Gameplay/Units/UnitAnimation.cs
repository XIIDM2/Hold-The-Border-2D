using UnityEngine;

public enum UnitAnimationParameter
{
    Horizontal,
    Vertical,
    IsMoving,
    IsAttacking,
    IsDead,
    IsDisolving,

}

[RequireComponent(typeof(Animator))]
public class UnitAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetFloat(UnitAnimationParameter parameter, float value)
    {
        _animator.SetFloat(parameter.ToString(), value);
    }

    public void SetBool(UnitAnimationParameter parameter, bool value)
    {
        _animator.SetBool(parameter.ToString(), value);
    }

    public void SetTrigger(UnitAnimationParameter parameter)
    {
        _animator.SetTrigger(parameter.ToString());
    }
}

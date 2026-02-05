using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Units
{
    public enum UnitAnimationParameter
    {
        IsMoving,
        Horizontal,
        Vertical,
        IsAttacking,
    }


    public class UnitAnimation : MonoBehaviour
    {
        private Animator _animator;

        private AnimatorOverrideController _animatorOverrideController;

        private List<KeyValuePair<AnimationClip, AnimationClip>> _clipOverrides;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();

            _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            _animator.runtimeAnimatorController = _animatorOverrideController;

        }

        public void Initialize<T>(T data) where T : AnimationOverrideData
        {
            _clipOverrides = new List<KeyValuePair<AnimationClip, AnimationClip>>(_animatorOverrideController.overridesCount);
            _animatorOverrideController.GetOverrides(_clipOverrides);

            for (int i = 0; i < _clipOverrides.Count; ++i)
            {
                _clipOverrides[i] = new KeyValuePair<AnimationClip, AnimationClip>(_clipOverrides[i].Key, data.GetOverrideClip(_clipOverrides[i].Key));
            }

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
}
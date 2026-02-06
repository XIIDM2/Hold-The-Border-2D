using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Units
{
    public class UnitAnimation : MonoBehaviour
    {
        public readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        public readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");
        public readonly int HorizontalHash = Animator.StringToHash("Horizontal");
        public readonly int VerticalHash = Animator.StringToHash("Vertical");

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


        public void SetFloat(int parameterHash, float value)
        {
            _animator.SetFloat(parameterHash, value);

        }

        public void SetBool(int parameterHash, bool value)
        {
            _animator.SetBool(parameterHash, value);
        }

        public void SetTrigger(int parameterHash)
        {
            _animator.SetTrigger(parameterHash);
        }

    }
}
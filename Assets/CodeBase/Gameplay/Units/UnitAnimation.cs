using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Units
{
    public class UnitAnimation : MonoBehaviour
    {
        public event UnityAction DeathAnimationComplete;
        public event UnityAction DissolveAnimationComplete;

        public readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        public readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");
        public readonly int HorizontalHash = Animator.StringToHash("Horizontal");
        public readonly int VerticalHash = Animator.StringToHash("Vertical");
        public readonly int IsDeadHash = Animator.StringToHash("IsDead");
        public readonly int IsDissolvingHash = Animator.StringToHash("IsDissolving");

        public bool CanDissolve => _canDissolve;

        [SerializeField] private bool _canDissolve;

        private Animator _animator;

        private AnimatorOverrideController _animatorOverrideController;

        private List<KeyValuePair<AnimationClip, AnimationClip>> _clipOverrides;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();

            _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            _animator.runtimeAnimatorController = _animatorOverrideController;

        }

        public void Init<T>(T data) where T : AnimationOverrideData
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

        public void NotifyDeathAnimationComplete()
        {
            DeathAnimationComplete?.Invoke();
        }

        public void NotifyDissolveAnimationComplete()
        {
            DissolveAnimationComplete?.Invoke();
        }

    }
}
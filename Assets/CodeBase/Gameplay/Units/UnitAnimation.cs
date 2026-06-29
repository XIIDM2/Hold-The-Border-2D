using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Units
{
    public class UnitAnimation : BaseAnimation
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
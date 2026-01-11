using Data;
using Gameplay.Units;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Towers.Units
{
    public class TowerUnitAnimation : UnitAnimation
    {
        public event UnityAction AttackAnimationEvent;

        [SerializeField] private AnimationData _data;

        private ObjectDirection _direction;

        protected override void Awake()
        {
            base.Awake();
            _direction = new ObjectDirection();
        }


        private void Start()
        {
            Init(_data);
        }


        private void Update()
        {

        }

        public void PlayAttackAnimation()
        {
            SetTrigger(UnitAnimationParameter.IsAttacking.ToString());
        }

        public void AEAttack()
        {
            AttackAnimationEvent?.Invoke();
        }
    }
}
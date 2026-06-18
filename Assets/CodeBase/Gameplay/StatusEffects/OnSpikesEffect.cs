using Gameplay.Units.Enemy;

namespace Gameplay.StatusEffects
{
    public class OnSpikesEffect : StatusEffect
    {
        private float _tickCooldown;
        private int _damage;
        private int _slowPercentage;

        private float _timer;

        public OnSpikesEffect(float tickCooldown, int damage, int slowPercentage)
        {
            _tickCooldown = tickCooldown;
            _damage = damage;
            _slowPercentage = slowPercentage;
        }

        public override void OnApply(EnemyUnitController target)
        {
            base.OnApply(target);
            _enemy.Health.TakeDamage(_damage);
            _enemy.Movement.ModifyMovementSpeed(-_slowPercentage);
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            _timer += deltaTime;

            if (_timer >= _tickCooldown)
            {
                _enemy.Health.TakeDamage(_damage);
                _timer = 0;
            }
        }

        public override void OnRemove()
        {
            base.OnRemove();
            _enemy.Movement.ResetMovementSpeed();
        }
    }
}
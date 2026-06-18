using Gameplay.Units.Enemy;

namespace Gameplay.StatusEffects
{
    public abstract class StatusEffect
    {
        public bool IsFinished { get; private set; }

        protected EnemyUnitController _enemy;

        public virtual void OnApply(EnemyUnitController target) => _enemy = target;
        public virtual void OnUpdate(float deltaTime) { }
        public virtual void OnRemove() { }
    }
}
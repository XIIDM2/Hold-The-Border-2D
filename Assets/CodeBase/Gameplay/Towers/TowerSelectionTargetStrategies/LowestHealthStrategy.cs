using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Gameplay.Towers.TargetSelectionStrategies
{
    public class LowestHealthStrategy : ITowerSelectionTargetStrategy
    {
        public ITargetable SelectTarget(IReadOnlyCollection<ITargetable> targetables)
        {
            if (targetables.Count == 0) return null;

            ITargetable target = null;
            int lowestHealth = int.MaxValue;

            foreach (ITargetable targetable in targetables)
            {
                if (lowestHealth > targetable.Health.CurrentHealth)
                {
                    lowestHealth = targetable.Health.CurrentHealth;
                    target = targetable;
                }
            }

            return target;
        }
    }
}
using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Gameplay.Towers.TargetSelectionStrategies
{
    public class LowestHealthStrategy : ITowerSelectionTargetStrategy
    {
        public ITargetable SelectTarget(List<ITargetable> targetables)
        {
            if (targetables.Count == 0) return null;

            ITargetable target = targetables[0];

            foreach (ITargetable targetable in targetables)
            {
                if (target.Health.CurrentHealth > targetable.Health.CurrentHealth)
                {
                    target = targetable;
                }
            }

            return target;
        }
    }
}
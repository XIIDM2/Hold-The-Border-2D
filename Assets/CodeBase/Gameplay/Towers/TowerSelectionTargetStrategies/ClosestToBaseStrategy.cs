using Infrastructure.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Towers.TargetSelectionStrategies
{
    public class ClosestToBaseStrategy : ITowerSelectionTargetStrategy
    {
        private Vector2 _basePosition;

        public ClosestToBaseStrategy(Vector2 basePosition)
        {
            _basePosition = basePosition;
        }

        public ITargetable SelectTarget(List<ITargetable> targetables)
        {
            if (targetables.Count == 0) return null;

            ITargetable target = targetables[0];

            foreach (ITargetable targetable in targetables)
            {
                if (Vector2.Distance(target.Position, _basePosition) > Vector2.Distance(targetable.Position, _basePosition))
                {
                    target = targetable;
                }
            }

            return target;
        }
    }
}
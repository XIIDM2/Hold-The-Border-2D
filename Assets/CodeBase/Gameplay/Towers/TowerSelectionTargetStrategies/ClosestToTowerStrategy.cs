using Infrastructure.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Towers.TargetSelectionStrategies
{
    public class ClosestToTowerStrategy : ITowerSelectionTargetStrategy
    {
        private Vector2 _towerPosition;

        public ClosestToTowerStrategy(Vector2 towerPosition)
        {
            _towerPosition = towerPosition;
        }

        public ITargetable SelectTarget(IReadOnlyCollection<ITargetable> targetables)
        {
            if (targetables.Count == 0) return null;

            ITargetable target = null;
            float closestDistance = float.MaxValue;

            foreach (ITargetable targetable in targetables)
            {
                float currentDistance = Vector2.Distance(targetable.Position, _towerPosition);
                if (closestDistance > currentDistance)
                {
                    closestDistance = currentDistance;
                    target = targetable;
                }
            }

            return target;
        }
    }
}
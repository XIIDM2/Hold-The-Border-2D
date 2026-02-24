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

        public ITargetable SelectTarget(List<ITargetable> targetables)
        {
            if (targetables.Count == 0) return null;

            ITargetable target = targetables[0];

            foreach (ITargetable targetable in targetables)
            {
                if (Vector2.Distance(target.Position, _towerPosition) > Vector2.Distance(targetable.Position, _towerPosition))
                {
                    target = targetable;
                }
            }

            return target;
        }
    }
}
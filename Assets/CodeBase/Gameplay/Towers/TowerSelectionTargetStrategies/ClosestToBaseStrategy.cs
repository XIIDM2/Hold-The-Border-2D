using Gameplay.Units.Enemy;
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
            int closestWaypointIndex = -1;

            foreach (ITargetable targetable in targetables)
            {
                if (targetable is EnemyUnitController enemy)
                {
                    if (closestWaypointIndex < enemy.Pathing.CurrentWaypointIndex)
                    {
                        closestWaypointIndex = enemy.Pathing.CurrentWaypointIndex;
                        target = targetable;
                    }
                }
                else
                {
                    // TODO: When neutural objects will be added, need to add logic here
                    continue;
                }
 
            }

            return target;
        }
    }
}
using Gameplay.Units.Enemy;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Towers.TargetSelectionStrategies
{
    public class ClosestToBaseStrategy : ITowerSelectionTargetStrategy
    {
        public ITargetable SelectTarget(List<ITargetable> targetables)
        {
            if (targetables.Count == 0) return null;

            ITargetable target = null;
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
                    else if (closestWaypointIndex == enemy.Pathing.CurrentWaypointIndex)
                    {
                        float currentDist = Vector2.Distance(target.Position, enemy.Pathing.CurrentWaypoint.transform.position);
                        float targetDist = Vector2.Distance(enemy.transform.position, enemy.Pathing.CurrentWaypoint.transform.position);

                        target = currentDist >= targetDist ? target : enemy;
                    }
                }
                else
                {
                    // TODO: When neutural objects will be added, need to add logic here
                    continue;
                }
 
            }
            Debug.Log($"Selected: ID={target.GetHashCode()} waypoint={closestWaypointIndex} pos={target.Position}");
            return target;
        }
    }
}
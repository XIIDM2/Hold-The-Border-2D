using Gameplay.Units.Enemy;
using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Gameplay.Towers.TargetSelectionStrategies
{
    public class ClosestToBaseStrategy : ITowerSelectionTargetStrategy
    {
        public ITargetable SelectTarget(List<ITargetable> targetables)
        {
            if (targetables.Count == 0) return null;

            ITargetable currentTarget = targetables[0];
            int higestWaypointIndex = -1;
            float closestDistanceWaypoint = float.MaxValue;

            foreach (ITargetable potentialTarget in targetables)
            {
                if (potentialTarget is EnemyUnitController potentialEnemy)
                {
                    int currentWaypointIndex = potentialEnemy.Pathing.CurrentWaypointIndex;
                    float currentDistanceWaypoint = (potentialEnemy.transform.position - potentialEnemy.Pathing.CurrentWaypoint.transform.position).sqrMagnitude;

                    if (higestWaypointIndex < currentWaypointIndex)
                    {
                        higestWaypointIndex = currentWaypointIndex;
                        closestDistanceWaypoint = currentDistanceWaypoint;
                        currentTarget = potentialTarget;
                    }
                    else if (higestWaypointIndex == currentWaypointIndex)
                    {
                        if (closestDistanceWaypoint > currentDistanceWaypoint)
                        {
                            closestDistanceWaypoint = currentDistanceWaypoint;
                            currentTarget = potentialTarget;
                        }
                    }
                }
            }

            return currentTarget;
        }
    }

}
using Gameplay.Path;
using UnityEngine;

namespace Gameplay.Units.Enemy
{
    public class EnemyUnitPathing : MonoBehaviour
    {
        public Vector2 WaypointPosition { get; private set; }
        public Waypoint CurrentWaypoint { get; private set; }

        [SerializeField] private float _offset = 1.0f;
        [SerializeField] private float _distance = 0.2f;


        public void Init(Waypoint startWaypoint)
        {
            CurrentWaypoint = startWaypoint;
            AddRandomOffset();
        }

        public bool CheckWaypointReached()
        {
            return Vector2.Distance(transform.position, WaypointPosition) < _distance;
        }

        public void SetNextWaypoint()
        {
            CurrentWaypoint = CurrentWaypoint.Next;

            AddRandomOffset();
        }

        public bool HasReachedEnd()
        {
            return CurrentWaypoint == null;
        }

        private void AddRandomOffset()
        {
            if (CurrentWaypoint != null) WaypointPosition = Random.insideUnitCircle * _offset + (Vector2)CurrentWaypoint.transform.position;
        }

    }
}
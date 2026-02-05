using Gameplay.Path;
using UnityEngine;

namespace Gameplay.Units.Enemy
{
    public class EnemyUnitPathing : MonoBehaviour
    {
        public Vector2 WaypointPosition { get; private set; }

        [SerializeField] private float _offset = 1.0f;
        [SerializeField] private float _distance = 0.2f;

        private Waypoint _currentWaypoint;

        public void Initialize(Waypoint startWaypoint)
        {
            _currentWaypoint = startWaypoint;
            AddRandomOffset();
        }

        public bool CheckWaypointReached()
        {
            return Vector2.Distance(transform.position, WaypointPosition) < _distance;
        }

        public void SetNextWaypoint()
        {
            _currentWaypoint = _currentWaypoint.Next;

            AddRandomOffset();
        }

        public bool HasReachedEnd()
        {
            return _currentWaypoint == null;
        }

        private void AddRandomOffset()
        {
            if (_currentWaypoint != null) WaypointPosition = Random.insideUnitCircle * _offset + (Vector2)_currentWaypoint.transform.position;
        }

    }
}
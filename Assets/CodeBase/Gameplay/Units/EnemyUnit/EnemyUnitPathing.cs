using UnityEngine;

namespace Gameplay.Units.Enemy
{
    public class EnemyUnitPathing : MonoBehaviour
    {
        public Vector2 CurrentPosition { get; private set; }

        [SerializeField] private float _offset = 1.0f;
        [SerializeField] private float _distance = 0.2f;

        private Waypoint _current;

        public void Init(Waypoint start)
        {
            _current = start;
            AddRandomOffset();
        }

        public bool CheckWaypointReached()
        {
            return Vector2.Distance(transform.position, CurrentPosition) < _distance;
        }

        public void SetNextWaypoint()
        {
            _current = _current.Next;

            AddRandomOffset();
        }

        public bool HasReachedEnd()
        {
            return _current == null;
        }

        private void AddRandomOffset()
        {
            if (_current != null) CurrentPosition = Random.insideUnitCircle * _offset + (Vector2)_current.transform.position;
        }

    }
}
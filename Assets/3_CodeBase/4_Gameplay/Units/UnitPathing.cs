using UnityEngine;

public class UnitPathing : MonoBehaviour
{
    public Vector2 CurrentPosition { get; private set; }

    [SerializeField] private Waypoint _current;

    [SerializeField] private float _distance = 0.5f;
    [SerializeField] private float _offset = 1.0f;

    private void Start()
    {
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
        return (_current == null);
    }

    private void AddRandomOffset()
    {
        if (_current != null) CurrentPosition = Random.insideUnitCircle * _offset + (Vector2)_current.transform.position;
    }

}

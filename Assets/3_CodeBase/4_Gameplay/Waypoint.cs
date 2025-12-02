using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint Next => _next;

    [SerializeField] private Waypoint _next;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(transform.position, 0.5f);

        if (_next != null) Gizmos.DrawLine(transform.position, _next.transform.position);
    }
}

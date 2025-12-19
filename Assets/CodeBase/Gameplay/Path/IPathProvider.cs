using UnityEngine;

public interface IPathProvider
{
    Waypoint GetWaypoint(PathType path);
}

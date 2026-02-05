namespace Gameplay.Path
{
    public interface IPathProvider
    {
        Waypoint GetWaypoint(PathType path);
    }
}
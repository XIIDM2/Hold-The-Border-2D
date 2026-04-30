namespace Gameplay.Path
{
    public interface IPathProvider
    {
        void Init();
        Waypoint GetWaypoint(PathType path);
    }
}
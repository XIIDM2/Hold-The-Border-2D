using System.Collections.Generic;
using UnityEngine;

public class PathService : MonoBehaviour, IPathProvider
{
    private readonly Dictionary<PathType, Waypoint> _pathsDict = new Dictionary<PathType, Waypoint>();

    [System.Serializable]
    public struct Path
    {
        public PathType Type => _type;
        public Waypoint Start => _start;

        [SerializeField] private PathType _type;
        [SerializeField] private Waypoint _start;
    }

    [SerializeField] private Path[] _paths;

    private void Awake()
    {
        foreach (Path path in _paths)
        {
            _pathsDict.TryAdd(path.Type, path.Start);
        }
    }

    public Waypoint GetWaypoint(PathType path)
    {
        if (_pathsDict.TryGetValue(path, out  Waypoint waypoint))
        {
            return waypoint;
        }

        Debug.LogError($"Could not retrieve {path} from dict");
        return null;
    }
}

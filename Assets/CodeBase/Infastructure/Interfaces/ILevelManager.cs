using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Managers
{
    public interface ILevelManager
    {
        event UnityAction Victory;
        event UnityAction Defeat;
        GameObject GameObject { get; }
    }
}
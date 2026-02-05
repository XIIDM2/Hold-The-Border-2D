using UnityEngine;

namespace Infrastructure.Interfaces
{
    public interface ITargetable
    {
        Vector2 Position { get; }
        IDamageable Health { get; }
    }
}
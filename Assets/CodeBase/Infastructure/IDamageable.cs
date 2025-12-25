using UnityEngine;
using UnityEngine.Events;

public interface IDamageable
{
    Health Health { get; }
    Vector2 Position { get; }
}

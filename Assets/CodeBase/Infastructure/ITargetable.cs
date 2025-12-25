using UnityEngine;

public interface ITargetable
{
    Vector2 Position { get; }
    IDamageable Damageable { get; }
}

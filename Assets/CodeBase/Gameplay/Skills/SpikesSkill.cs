using Infrastructure.Interfaces;
using UnityEngine;

public class SpikesSkill : MonoBehaviour, ISkill
{
    [SerializeField] private float _tickCooldown = 1f;

    private int _damage;
    private int _slowPercentage;
    private float _duration;
    private float _radius;

    public void Init(int damage, int slowPercentage, float duration, float radius)
    {
        _damage = damage;
        _slowPercentage = slowPercentage;
        _duration = duration;
        _radius = radius;
    }
}

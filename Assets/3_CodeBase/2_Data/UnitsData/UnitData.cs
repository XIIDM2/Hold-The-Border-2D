using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Objects/Units/Unit Data")]
public class UnitData : ScriptableObject
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _attackDamage;
    [SerializeField] private int _pathEndDamage;

    public int MaxHealth => _maxHealth;
    public float MovementSpeed => _movementSpeed;
    public int AttackDamage => _attackDamage;
    public int PathEndDamage => _pathEndDamage;
}

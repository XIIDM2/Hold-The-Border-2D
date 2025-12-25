using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Objects/Units/Unit Data")]
public class UnitData : ScriptableObject
{
    [SerializeField] private UnitType _type;
    [SerializeField] private AssetReferenceGameObject _prefabReference;

    [SerializeField] private int _maxHealth;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _attackDamage;
    [SerializeField] private int _pathEndDamage;
    [SerializeField] private float _attackCooldown;

    [SerializeField] private UnitAnimationsData _animations;

    public UnitType Type => _type;
    public AssetReferenceGameObject PrefabReference => _prefabReference;
    public int MaxHealth => _maxHealth;
    public float MovementSpeed => _movementSpeed;
    public int AttackDamage => _attackDamage;
    public int PathEndDamage => _pathEndDamage;
    public float AttackCooldown => _attackCooldown;
    public UnitAnimationsData Animations => _animations;
}

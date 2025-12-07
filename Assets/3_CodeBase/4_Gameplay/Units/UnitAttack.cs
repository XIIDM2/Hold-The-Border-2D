using TriInspector;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    public int PathEndDamage {  get; private set; }
    [ShowInInspector, ReadOnly]
    public int AttackDamage {  get; private set; }
    public void Init(int pathEndDamage, int attackDamage)
    {
        PathEndDamage = pathEndDamage;
        AttackDamage = attackDamage;
    }

    public void Attack()
    {

    }
}

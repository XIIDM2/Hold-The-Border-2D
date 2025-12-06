using NaughtyAttributes;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    [ShowNativeProperty]
    public int PathEndDamage {  get; private set; }
    [ShowNativeProperty]
    public int AttackDamage {  get; private set; }

    public void Init(int damage)
    {
        PathEndDamage = damage;
    }

    public void Attack()
    {

    }
}

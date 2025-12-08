
using UnityEngine;

public class UnitAttack : MonoBehaviour
{

    public int PathEndDamage {  get; private set; }

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

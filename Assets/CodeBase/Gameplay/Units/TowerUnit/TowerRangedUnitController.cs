using UnityEngine;

public class TowerRangedUnitController : TowerUnitController
{
    public override void Init(TowerUnitAnimationData data)
    {
        throw new System.NotImplementedException();
    }

    public override void RequestAttack()
    {
        Debug.Log("Attacking");
    }
}

using UnityEngine;

public abstract class TowerUnitController : BaseUnitController<TowerUnitController>, IAttacker
{
    public abstract void Init(TowerUnitAnimationData data);

    public abstract void RequestAttack();

}

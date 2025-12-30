using UnityEngine;

public class TowerRangedUnitIdleState : TowerRangedUnitState
{
    public override void Update(TowerRangedUnitController controller)
    {
        base.Update(controller);

        Debug.Log("Im in idle state");
    }
}

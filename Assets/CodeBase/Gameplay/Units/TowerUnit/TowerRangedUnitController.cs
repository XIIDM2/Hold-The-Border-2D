using UnityEngine;

public class TowerRangedUnitController : BaseUnitController<TowerRangedUnitController>, IAttacker
{
    [Header("FSM")]
    public TowerRangedUnitIdleState IdleState { get; private set; }

    private void Start()
    {
        IdleState = new TowerRangedUnitIdleState();

        ActionFSM = new FiniteStateMachine<TowerRangedUnitController>();
        ActionFSM.StateInit(IdleState, this);
    }

    public void Init(AnimationData animations, int damage, float coolDown)
    {
        if (Animation) Animation.Init(animations);
        if (Attack) Attack.Init(damage, coolDown);
    }

    public void ExecuteAttack()
    {
        Debug.Log("Attacking!");
    }
}

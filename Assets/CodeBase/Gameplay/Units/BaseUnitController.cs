using UnityEngine;

public abstract class BaseUnitController<T> : MonoBehaviour, IControllable where T : BaseUnitController<T>
{
    [Header("Components")]
    public BaseUnitAttack Attack { get; protected set; }
    public BaseUnitAnimation Animation { get; protected set; }

    [Header("FSM")]
    public FiniteStateMachine<T> ActionFSM { get; protected set; }

    protected virtual void Awake()
    {
        Animation = GetComponentInChildren<BaseUnitAnimation>();
    }

    protected virtual void Update()
    {
        ActionFSM.UpdateState((T)this);
    }

    protected virtual void LateUpdate()
    {
        ActionFSM.LateUpdateState((T)this);

    }

    protected virtual void FixedUpdate()
    {
        ActionFSM.FixedUpdateState((T)this);
    }
}

using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(UnitPathing))]
public class UnitController : MonoBehaviour, IControllable
{
    [Header("Components")]
    public Health Health { get; private set; }
    public UnitMovement Movement {  get; private set; }
    public UnitAnimation Animation { get; private set; }

    public UnitPathing Pathing { get; private set; }

    [Header("FSM")]
    public FiniteStateMachine<UnitController> ActionFSM { get; private set; }
    public UnitState MoveState { get; private set; }

    private void Awake()
    {        
        Movement = GetComponent<UnitMovement>();
        Animation = GetComponentInChildren<UnitAnimation>();
        Pathing = GetComponent<UnitPathing>();

        Health = GetComponent<Health>();
    }

    private void Start()
    {
        MoveState = new UnitMoveState();

        ActionFSM = new FiniteStateMachine<UnitController>();
        ActionFSM.StateInit(MoveState, this);
    }

    private void Update()
    {
        ActionFSM.UpdateState(this);
    }

    private void LateUpdate()
    {
        ActionFSM.LateUpdateState(this);

    }

    private void FixedUpdate()
    {
        ActionFSM.FixedUpdateState(this);
    }

    public void Init(Waypoint start)
    {
        Pathing.Init(start);
    }

    public void DestroyUnit()
    {
        Destroy(gameObject);
    }

}

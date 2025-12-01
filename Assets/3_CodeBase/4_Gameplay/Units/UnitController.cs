using UnityEngine;

public class UnitController : MonoBehaviour, IControllable
{
    public Transform _testTransform;

    [Header("Components")]
    public UnitMovement Movement {  get; private set; }
    public Health Health { get; private set; }
    public UnitAnimation Animation { get; private set; }

    [Header("FSM")]
    public FiniteStateMachine<UnitController> ActionFSM { get; private set; }
    public UnitState MoveState { get; private set; }

    private void Awake()
    {        
        Movement = GetComponent<UnitMovement>();
        Animation = GetComponentInChildren<UnitAnimation>();

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

}

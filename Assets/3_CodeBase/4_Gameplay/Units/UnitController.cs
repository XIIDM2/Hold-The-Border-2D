using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(UnitPathing))]
public class UnitController : MonoBehaviour, IControllable
{
    [Header("Components")]
    public Health Health { get; private set; }
    public UnitMovement Movement {  get; private set; }
    public UnitAttack Attack { get; private set; }
    public UnitPathing Pathing { get; private set; }
    public UnitAnimation Animation { get; private set; }


    [Header("FSM")]
    public FiniteStateMachine<UnitController> ActionFSM { get; private set; }
    public UnitState MoveState { get; private set; }

    private void Awake()
    {        
        Health = GetComponent<Health>();
        Movement = GetComponent<UnitMovement>();
        Attack = GetComponent<UnitAttack>();

        Pathing = GetComponent<UnitPathing>();

        Animation = GetComponentInChildren<UnitAnimation>();
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

    public void Init(UnitData data, Waypoint start)
    {
        Health.Init(data.MaxHealth);
        Movement.Init(data.MovementSpeed);
        Attack.Init(data.PathEndDamage, data.AttackDamage);
        Pathing.Init(start);
    }

    public void DestroyUnit()
    {
        Destroy(gameObject);
    }

}

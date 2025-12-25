using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(UnitPathing))]
public class UnitController : MonoBehaviour, IControllable, ITargetable
{
    public Vector2 Position => transform.position;

    [SerializeField, ReadOnly] private int _currentHealth;

    [Header("Components")]
    public IDamageable Damageable { get; private set; }
    public UnitMovement Movement {  get; private set; }
    public UnitAttack Attack { get; private set; }
    public UnitPathing Pathing { get; private set; }
    public UnitAnimation Animation { get; private set; }

    [Header("FSM")]
    public FiniteStateMachine<UnitController> ActionFSM { get; private set; }
    public UnitState MoveState { get; private set; }


    private void Awake()
    {        
        Damageable = new Health();

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

    private void OnEnable()
    {
        Damageable.OnDeath += DestroyUnit;
    }

    private void OnDisable()
    {
        Damageable.OnDeath -= DestroyUnit;
    }

    private void Update()
    {
        ActionFSM.UpdateState(this);
        _currentHealth = Damageable.CurrentHealth;
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
        Animation.Init(data.Animations);
        Damageable.Init(data.MaxHealth);
        Movement.Init(data.MovementSpeed);
        Attack.Init(data.PathEndDamage, data.AttackDamage, data.AttackCooldown);
        Pathing.Init(start);
    }

    public void DestroyUnit(IDamageable damageable)
    {
        Destroy(gameObject);
    }

}

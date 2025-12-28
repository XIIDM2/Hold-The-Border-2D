using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(EnemyUnitPathing))]
public class EnemyUnitController : BaseUnitController<EnemyUnitController>, ITargetable
{
    public Vector2 Position => transform.position;
    public int PathEndDamage {  get; private set; }

    [SerializeField, ReadOnly] private int _currentHealth;

    [Header("Components")]
    public IDamageable Damageable { get; private set; }
    public UnitMovement Movement { get; private set; }
    public EnemyUnitPathing Pathing { get; private set; }

    [Header("FSM")]
    public UnitMoveState MoveState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Damageable = new Health();
        Movement = GetComponent<UnitMovement>();
        Pathing = GetComponent<EnemyUnitPathing>();

    }
    private void Start()
    {
        MoveState = new UnitMoveState();

        ActionFSM = new FiniteStateMachine<EnemyUnitController>();
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

    protected override void Update()
    {
        base.Update();

        _currentHealth = Damageable.CurrentHealth;
    }

    public void Init(EnemyUnitData data, Waypoint start)
    {
        PathEndDamage = data.PathEndDamage;
        Animation.Init(data.Animations);
        Damageable.Init(data.MaxHealth);
        Movement.Init(data.MovementSpeed);
        Attack.Init(data.AttackDamage, data.AttackCooldown);
        Pathing.Init(start);
    }

    public void DestroyUnit(IDamageable damageable)
    {
        Destroy(gameObject);
    }

}

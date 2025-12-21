using UnityEngine;
using UnityEngine.Events;

public class TowerController : MonoBehaviour, IControllable
{
    public event UnityAction UpgradeRequested;
    public int CurrentTierIndex {  get; private set; }
    public int MaxTier => _data.TierConfigs.Length - 1;
    public TowerData.TowerTierConfig CurrentTierConfig => _data.TierConfigs[CurrentTierIndex];

    [SerializeField] private TowerData _data;

    [Header("Components")]
    public TowerDetection Detection {  get; private set; }
    public TowerAnimation Animation {  get; private set; }

    [Header("FSM")]
    public FiniteStateMachine<TowerController> ActionFSM { get; private set; }
    public TowerUpgradeState UpgradeState { get; private set; }
    public TowerIdleState IdleState { get; private set; }

    private void Awake()
    {
        Detection = GetComponentInChildren<TowerDetection>();
        Animation = GetComponentInChildren<TowerAnimation>();
    }

    private void Start()
    {
        Init();

        UpgradeState = new TowerUpgradeState();
        IdleState = new TowerIdleState();

        ActionFSM = new FiniteStateMachine<TowerController>();
        ActionFSM.StateInit(IdleState, this);
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

    public void Init()
    {
        Detection.Init(CurrentTierConfig.AttackRadius);
        ApplyCurrentTier();
        // первичная инициализация
    }

    public void ApplyCurrentTier()
    {
        Animation.UpgradeTowerAnimations(CurrentTierConfig.UpgradeAnimation, CurrentTierConfig.IdleAnimation);
        // назначение данных
    }

    public void MoveToNextTier()
    {
        if (CurrentTierIndex >= MaxTier) return;

        CurrentTierIndex++;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 200, 150, 100), "Upgrade Tower"))
        {
            UpgradeRequested?.Invoke();
        }
    }

}

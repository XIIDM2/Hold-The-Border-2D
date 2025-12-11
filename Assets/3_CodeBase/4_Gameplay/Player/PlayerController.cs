using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Health _health;

     [SerializeField] private PlayerData _data;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        _health.Init(_data.MaxHeath);
    }

    private void OnEnable()
    {
        Messenger<int>.AddListener(Events.UnitReachedEnd, _health.TakeDamage);
    }

    private void OnDisable()
    {
        Messenger<int>.RemoveListener(Events.UnitReachedEnd, _health.TakeDamage);
    }
}

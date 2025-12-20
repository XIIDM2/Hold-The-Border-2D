using System.Collections;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public bool IsUpgrading {  get; private set; }
    public TowerAnimation Animation {  get; private set; }

    [SerializeField] private TowerData _data;

    private Coroutine _upgradeCoroutine;
    private int _currentTierIndex = 0;

    private void Awake()
    {
        Animation = GetComponentInChildren<TowerAnimation>();
    }

    private void OnDisable()
    {
        IsUpgrading = false;
        _upgradeCoroutine = null;
    }

    public void TryUpgrade()
    {
        if (IsUpgrading)
        {
            Debug.Log("Tower currently upgrading, Cannot upgrade tower");
            return;
        }

        if (_currentTierIndex >= _data.TierConfigs.Length)
        {
            Debug.Log("Tower tier is maximum! Cannot upgrade tower");
            return;
        }

        _upgradeCoroutine = StartCoroutine(UpgradeRoutine());
    }

    private IEnumerator UpgradeRoutine()
    {
        IsUpgrading = true;

        TowerData.TowerTierConfig tierConfig = _data.TierConfigs[_currentTierIndex];

        Animation.PlayUpgradeAnimation(tierConfig.UpgradeAnimation);
        Animation.ChangeIdleAnimation(tierConfig.IdleAnimation);

        yield return new WaitForSeconds(tierConfig.UpgradeAnimation.length);

        _currentTierIndex++;

        IsUpgrading = false;
        _upgradeCoroutine = null;

    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Upgrade Tower"))
        {
            TryUpgrade();
        }
    }

}

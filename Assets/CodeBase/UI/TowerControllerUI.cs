using Gameplay.Towers;
using UnityEngine;

public class TowerControllerUI : MonoBehaviour
{
    private TowerController _tower;

    private void OnEnable()
    {
        Messenger<TowerController>.AddListener(Events.TowerClicked, SetTower);
    }

    private void OnDisable()
    {
        Messenger<TowerController>.RemoveListener(Events.TowerClicked, SetTower);
    }

    private void SetTower(TowerController tower)
    {
        _tower = tower;
    }

    public void Upgrade()
    {
        if (_tower) Messenger<TowerController>.Broadcast(Events.TowerUpgradeRequested, _tower);
    }

    public void Sell()
    {
        if (_tower) Messenger<TowerController>.Broadcast(Events.TowerSellRequested, _tower);
    }

}

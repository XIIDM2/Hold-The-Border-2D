using UnityEngine;

public class TowerIdleState : TowerState
{
    private bool _towerUpgradeRequested;

    public override State<TowerController> HandleTransitions(TowerController controller)
    {
        if (_towerUpgradeRequested)
        {
            _towerUpgradeRequested = false;

            if (controller.CurrentTierIndex >= controller.MaxTier) return null;

            return controller.UpgradeState;
        }

        return base.HandleTransitions(controller);
    }

    public override void Enter(TowerController controller)
    {
        base.Enter(controller);

        controller.UpgradeRequested += OnTowerUpgradeRequested;
    }

    public override void Exit(TowerController controller)
    {
        base.Exit(controller);

        controller.UpgradeRequested -= OnTowerUpgradeRequested;

    }
    private void OnTowerUpgradeRequested()
    {
        _towerUpgradeRequested = true;
    }
}

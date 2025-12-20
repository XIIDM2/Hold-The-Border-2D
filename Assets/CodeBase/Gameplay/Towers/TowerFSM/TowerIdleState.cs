using UnityEngine;

public class TowerIdleState : TowerState
{
    private bool isTowerUpgradeRequested;

    public override State<TowerController> HandleTransitions(TowerController controller)
    {
        if (isTowerUpgradeRequested)
        {
            if (controller.CurrentTierIndex >= controller.MaxTier) return null;

            isTowerUpgradeRequested = false;
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
        isTowerUpgradeRequested = true;
    }
}

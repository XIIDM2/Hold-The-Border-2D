using UnityEngine;

public class TowerUpgradeState : TowerState
{
    private bool _upgradeAnimationCompleted;
    public override State<TowerController> HandleTransitions(TowerController controller)
    {
        if (_upgradeAnimationCompleted) return controller.IdleState;

        return base.HandleTransitions(controller);
    }

    public override void Enter(TowerController controller)
    {
        base.Enter(controller);

        _upgradeAnimationCompleted = false;

        controller.MoveToNextTier();
        controller.ApplyCurrentTier();

        controller.Animation.PlayUpgradeAnimation();

        controller.Animation.UpgradeAnimationCompleted += OnUpgradeAnimationCompleted;
    }

    public override void Exit(TowerController controller)
    {
        base.Exit(controller);

        controller.Animation.UpgradeAnimationCompleted -= OnUpgradeAnimationCompleted;
    }

    private void OnUpgradeAnimationCompleted()
    {
        _upgradeAnimationCompleted = true;
    }

}

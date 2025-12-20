using UnityEngine;

public class TowerUpgradeState : TowerState
{
    private float _elapsed = 0.0f;
    private float _upgradeAnimationLength;

    public override State<TowerController> HandleTransitions(TowerController controller)
    {
        if (_elapsed >= _upgradeAnimationLength) return controller.IdleState;

        return base.HandleTransitions(controller);
    }

    public override void Enter(TowerController controller)
    {
        base.Enter(controller);

        _elapsed = 0.0f;

        _upgradeAnimationLength = controller.CurrentTierConfig.UpgradeAnimation.length;

        controller.MoveToNextTier();
        controller.ApplyCurrentTier();
        controller.Animation.PlayUpgradeAnimation();
    }

    public override void Update(TowerController controller)
    {
        base.Update(controller);

        _elapsed += Time.deltaTime;
    }

}

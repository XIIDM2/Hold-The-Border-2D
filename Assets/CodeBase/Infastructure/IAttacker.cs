using UnityEngine;

public interface IAttacker
{
    void Init(AnimationData animations, int damage, float coolDown);
    void ExecuteAttack();
}

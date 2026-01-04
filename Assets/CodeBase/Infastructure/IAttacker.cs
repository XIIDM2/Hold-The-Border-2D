using Data;
using UnityEngine;

namespace Infrastructure.Interfaces
{
    public interface IAttacker
    {
        void Init(AnimationData animations, int damage, float coolDown);
        void ExecuteAttack();
    }
}
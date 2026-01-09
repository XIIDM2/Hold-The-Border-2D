using Data;
using UnityEngine;

namespace Infrastructure.Interfaces
{
    public interface IAttacker
    {
        void Init(int damage, float coolDown);
        void ExecuteAttack(ITargetable target);
    }
}
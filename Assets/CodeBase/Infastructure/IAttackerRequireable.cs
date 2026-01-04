using Data;
using UnityEngine;

namespace Infrastructure.Interfaces
{
    public interface IAttackerRequireable
    {
        void Init(AnimationData animations, int damage, float coolDown);
    }
}
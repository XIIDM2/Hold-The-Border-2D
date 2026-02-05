using UnityEngine;

namespace Infrastructure.Interfaces
{
    public interface IProjectileRequireable
    {
        void InitProjectile(GameObject projectilePrefab);
    }
}
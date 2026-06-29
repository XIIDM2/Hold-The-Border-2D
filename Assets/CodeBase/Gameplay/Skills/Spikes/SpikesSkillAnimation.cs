using UnityEngine;

namespace Gameplay.Skills
{
    public class SpikesSkillAnimation : BaseAnimation
    {
        public readonly int IsDespawnHash = Animator.StringToHash("IsDespawn");

        public void Despawn()
        {
            SetBool(IsDespawnHash, true);
        }

        public void AEDespawn() => Destroy(gameObject);
        
    }
    
}
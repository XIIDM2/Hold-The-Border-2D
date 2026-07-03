using UnityEngine;

namespace Gameplay.Skills
{
    public class DeployableSkillAnimation : BaseAnimation
    {
        public readonly int IsDespawnHash = Animator.StringToHash("IsDespawn");

        public void Despawn()
        {
            SetBool(IsDespawnHash, true);
        }

        public void AEDespawn() => Destroy(transform.root.gameObject);
        
    }
    
}
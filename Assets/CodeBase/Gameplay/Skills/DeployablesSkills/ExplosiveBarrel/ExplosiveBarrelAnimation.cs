using System;
using UnityEngine;

namespace Gameplay.Skills
{
    public class ExplosiveBarrelAnimation : DeployableSkillAnimation
    {
        public event Action IsExploded;

        [SerializeField] private GameObject _explosionVFX;

        protected override void Awake()
        {
            base.Awake();
            _explosionVFX.SetActive(false);
        }

        public void AEEnableEplosionVFX() => _explosionVFX.SetActive(true);

        public void AEExplode() => IsExploded?.Invoke();
     
    }
}
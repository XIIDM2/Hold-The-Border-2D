using Data;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Towers.Units
{
    public class TowerUnitAnimation : UnitAnimation
    {
        [SerializeField] private AnimationData _animationData;

        private void Start()
        {
            Init(_animationData);
        }
    }
}
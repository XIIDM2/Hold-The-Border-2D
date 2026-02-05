using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Animation Override Data", menuName = "Scriptable Objects/Animations/Animation Override Data")]
    public class AnimationOverrideData : ScriptableObject
    {
        [System.Serializable]
        public class AnimationOverrideConfig
        {
            public AnimationClip BaseClip => _baseClip;
            public AnimationClip OverrideClip => _overrideClip;

            [SerializeField] private AnimationClip _baseClip;
            [SerializeField] private AnimationClip _overrideClip;
        }

        [SerializeField] private AnimationOverrideConfig[] _configs;

        private Dictionary<AnimationClip, AnimationClip> _overrideAnimations;

        private void OnEnable()
        {
            DictInit();
        }

        private void DictInit()
        {
            if (_configs == null || _configs.Length == 0)
            {
                Debug.LogError("Animations array is empty, cannot initialize Dictionary");
                return;
            }

            _overrideAnimations = new Dictionary<AnimationClip, AnimationClip>();

            foreach (AnimationOverrideConfig config in _configs)
            {
                if (!_overrideAnimations.TryAdd(config.BaseClip, config.OverrideClip))
                {
                    Debug.LogError($"Failed to add {config.OverrideClip} for {config.BaseClip}");
                }
            }
        }

        public AnimationClip GetOverrideClip(AnimationClip baseClip)
        {
            if (_overrideAnimations.TryGetValue(baseClip, out AnimationClip clip))
            {
                return clip;
            }

            //Debug.LogWarning($"Failed to retrieve override clip for {baseClip} from catalog");
            return null;
        }
    }
}
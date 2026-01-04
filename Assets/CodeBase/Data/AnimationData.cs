using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Animation Data", menuName = "Scriptable Objects/Animations/Animation Data")]
    public class AnimationData : ScriptableObject
    {
        [System.Serializable]
        public class AnimationsConfig
        {
            public AnimationClip BaseClip => _baseClip;
            public AnimationClip OverrideClip => _overrideClip;

            [SerializeField] private AnimationClip _baseClip;
            [SerializeField] private AnimationClip _overrideClip;
        }

        [SerializeField] private AnimationsConfig[] _configs;
        private Dictionary<AnimationClip, AnimationClip> _animationsDict;

        private void OnEnable()
        {
            DictInit();
        }

        private void DictInit()
        {
            if (_configs == null || _configs.Length == 0)
            {
                Debug.LogWarning("Animations array is empty, cannot initialize Dictionary");
                return;
            }

            _animationsDict = new Dictionary<AnimationClip, AnimationClip>();

            foreach (AnimationsConfig config in _configs)
            {
                if (!_animationsDict.TryAdd(config.BaseClip, config.OverrideClip))
                {
                    Debug.LogWarning($"Failed to add {config.OverrideClip} for {config.BaseClip}");
                }
            }
        }

        public AnimationClip GetClip(AnimationClip baseClip)
        {
            if (_animationsDict == null)
            {
                Debug.LogWarning("Clip dictionary is null, initializing dictionary...");
                DictInit();
            }

            if (_animationsDict.TryGetValue(baseClip, out AnimationClip clip))
            {
                return clip;
            }

            //Debug.LogWarning($"Failed to retrieve override clip for {baseClip} from catalog");
            return null;

        }

    }
}
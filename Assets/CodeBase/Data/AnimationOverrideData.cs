using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Animation Override Data", menuName = "ScriptableObjects/Animations/Animation Override Data")]
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

        // We are using string here because the SO and Animations are not included in Addressables, while base animator controller is bundled with addressable unit prefab.
        // Because of this, in build SO reference to clips are different from addressables.
        // In the result dictionary cant find any clip and animations not working properly.
        // Using strings (clip names) as a key we maintain security and resolving this issue.
        private Dictionary<string, AnimationClip> _overrideAnimations;

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

            _overrideAnimations = new Dictionary<string, AnimationClip>();

            foreach (AnimationOverrideConfig config in _configs)
            {
                if (!_overrideAnimations.TryAdd(config.BaseClip.name, config.OverrideClip))
                {
                    Debug.LogError($"Failed to add {config.OverrideClip} for {config.BaseClip}");
                }
            }
        }

        public AnimationClip GetOverrideClip(AnimationClip baseClip)
        {
            if (_overrideAnimations.TryGetValue(baseClip.name, out AnimationClip clip))
            {
                return clip;
            }

            //Debug.LogWarning($"Failed to retrieve override clip for {baseClip} from catalog");
            return null;
        }
    }
}
using Gameplay.Towers;
using Gameplay.Units.Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data
{
    [CreateAssetMenu(fileName = "Gameplay Registry", menuName = "Scriptable Objects/Catalogs/GameplayRegistry")]
    public class GameplayRegistry : ScriptableObject
    {
        public AssetReferenceGameObject BuildSiteReference => _buildSiteReference;
        public AssetReferenceGameObject DamagePopupReference => _damagePopupReference;
        public AssetReferenceGameObject TowerPanelReference => _towerPanelReference;

        public TowerData[] TowerDatas => _towerDatas;

        [SerializeField] private AssetReferenceGameObject _buildSiteReference;

        [Header("UI")]
        [SerializeField] private AssetReferenceGameObject _damagePopupReference;
        [SerializeField] private AssetReferenceGameObject _towerPanelReference;

        [SerializeField] private EnemyUnitData[] _unitDatas;
        [SerializeField] private TowerData[] _towerDatas;

        private Dictionary<EnemyUnitType, EnemyUnitData> _unitDataDict;
        private Dictionary<TowerType, TowerData> _towerDataDict;

        private void OnEnable()
        {
            UnitDictionaryInit();
            TowerDictionaryInit();
        }

        private void UnitDictionaryInit()
        {
            if (_unitDatas == null || _unitDatas.Length == 0)
            {
                Debug.LogWarning("Unit datas array is empty, can not initialize dictionary...");
                return;
            }

            _unitDataDict = new Dictionary<EnemyUnitType, EnemyUnitData>();

            foreach (EnemyUnitData unitData in _unitDatas)
            {
                if (!_unitDataDict.TryAdd(unitData.Type, unitData))
                {
                    Debug.LogWarning($"Failed to add {unitData} for {unitData.Type}");
                }
            }
        }

        private void TowerDictionaryInit()
        {
            if (_towerDatas == null || _towerDatas.Length == 0)
            {
                Debug.LogWarning("Tower datas array is empty, can not initialize dictionary...");
                return;
            }

            _towerDataDict = new Dictionary<TowerType, TowerData>();

            foreach (TowerData towerData in _towerDatas)
            {
                if (!_towerDataDict.TryAdd(towerData.Type, towerData))
                {
                    Debug.LogWarning($"Failed to add {towerData} for {towerData.Type}");
                }
            }
        }

        public EnemyUnitData GetUnitData(EnemyUnitType type)
        {
            if (_unitDataDict == null)
            {
                Debug.LogWarning("Unit dictionary is null, initializing dictionary...");
                UnitDictionaryInit();
            }

            if (_unitDataDict.TryGetValue(type, out EnemyUnitData data))
            {
                return data;
            }

            Debug.LogError($"Failed to retrieve {type} data from catalog");
            return null;
        }

        public TowerData GetTowerData(TowerType type)
        {
            if (_towerDataDict == null)
            {
                Debug.LogWarning("Tower dictionary is null, initializing dictionary...");
                UnitDictionaryInit();
            }

            if (_towerDataDict.TryGetValue(type, out TowerData data))
            {
                return data;
            }

            Debug.LogError($"Failed to retrieve {type} data from catalog");
            return null;
        }
    }
}
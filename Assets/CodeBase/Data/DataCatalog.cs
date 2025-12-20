using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Catalog", menuName = "Scriptable Objects/Catalogs/DataCatalog")]
public class DataCatalog : ScriptableObject
{
    [SerializeField] private UnitData[] _unitDatas;
    [SerializeField] private TowerData[] _towerDatas;

    private Dictionary<UnitType, UnitData> _unitDataDict;
    private Dictionary<TowerType, TowerData> _towerDataDict;

    private void OnEnable()
    {
        UnitDictionaryInit();
        TowertDictionaryInit();
    }

    private void UnitDictionaryInit()
    {
        if (_unitDatas == null || _unitDatas.Length == 0)
        {
            Debug.LogWarning("Unit datas array is empty, can not initialize dictionary...");
            return;
        }

        _unitDataDict = new Dictionary<UnitType, UnitData>();

        foreach (UnitData unitData in _unitDatas)
        {
            if (_unitDataDict.ContainsKey(unitData.Type))
            {
                Debug.LogWarning($"{unitData.Type} data already in unit dictionary, skipping...");
                continue;
            }

            _unitDataDict[unitData.Type] = unitData;
        }
    }

    private void TowertDictionaryInit()
    {
        if (_towerDataDict == null || _towerDatas.Length == 0)
        {
            Debug.LogWarning("Tower datas array is empty, can not initialize dictionary...");
            return;
        }

        _towerDataDict = new Dictionary<TowerType, TowerData>();

        foreach (TowerData towerData in _towerDatas)
        {
            if (_towerDataDict.ContainsKey(towerData.Type))
            {
                Debug.LogWarning($"{towerData.Type} data already in tower dictionary, skipping...");
                continue;
            }

            _towerDataDict[towerData.Type] = towerData;
        }
    }

    public UnitData GetUnitData(UnitType type)
    {
        if (_unitDataDict == null)
        {
            Debug.LogWarning("Unit dictionary is null, initializing dictionary...");
            UnitDictionaryInit();
        }

        if (_unitDataDict.TryGetValue(type, out UnitData data))
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

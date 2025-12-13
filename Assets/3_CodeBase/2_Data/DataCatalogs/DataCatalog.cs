using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Catalog", menuName = "Scriptable Objects/Catalogs/DataCatalog")]
public class DataCatalog : ScriptableObject
{
    [SerializeField] private UnitData[] _unitDatas;

    private Dictionary<UnitType, UnitData> _unitDataDict;

    private void OnEnable()
    {
        UnitDictionaryInit();
    }

    private void UnitDictionaryInit()
    {
        if (_unitDatas == null || _unitDatas.Length == 0)
        {
            Debug.LogWarning("Unit datas array is empty, can not init dictionary...");
            return;
        }

        _unitDataDict = new Dictionary<UnitType, UnitData>();

        foreach (var unitData in _unitDatas)
        {
            if (_unitDataDict.ContainsKey(unitData.Type))
            {
                Debug.LogWarning("Unit Data already in Unit dictionary, skipping...");
                continue;
            }

            _unitDataDict[unitData.Type] = unitData;
        }
    }

    public UnitData GetUnitData(UnitType type)
    {
        if (_unitDataDict == null)
        {
            Debug.LogWarning("Unit Dictionary is null, initializing dictionary...");
            UnitDictionaryInit();
        }

        if (_unitDataDict.TryGetValue(type, out UnitData data))
        {
            return data;    
        }

        Debug.LogError("Failed to retrieve unit data from catalog");
        return null;
    }
}

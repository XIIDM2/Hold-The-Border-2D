using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UnitFactory
{
    private Dictionary<string, GameObject> _cachedUnits = new Dictionary<string, GameObject>();
    private Dictionary<string, UnitData> _cachedUnitsData = new Dictionary<string, UnitData>();

    public async UniTask<GameObject> CreateUnit(UnitType type, Waypoint start, Vector2 position)
    {
        UnitData unitData = await LoadUnitData(type);

        GameObject unit = Object.Instantiate(await LoadUnit(type), position, Quaternion.identity);

        unit.GetComponent<UnitController>().Init(unitData, start);

        return unit;

    }

    private async UniTask<GameObject>LoadUnit(UnitType type)
    {
        string label = type.ToString();

        if (_cachedUnits.TryGetValue(label, out GameObject cached))
        {
            return cached;
        }

        try
        {
            GameObject loaded = await Addressables.LoadAssetAsync<GameObject>(label).ToUniTask();

            _cachedUnits[label] = loaded;

            return loaded;
        }
        catch(System.Exception ex) 
        {
            Debug.LogError("Failed to download unit from Addressables with exception: " + ex);

            return null;
        }
        
    }

    private async UniTask<UnitData> LoadUnitData(UnitType type)
    {
        string label = type.ToString();

        if (_cachedUnitsData.TryGetValue(label, out UnitData cached))
        {
            return cached;
        }

        try
        {
            UnitData loaded = await Addressables.LoadAssetAsync<UnitData>(label).ToUniTask();

            _cachedUnitsData[label] = loaded;

            return loaded;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to download unit data from Addressables with exception: " + ex);

            return null;
        }

    }

    public void ReleaseUnitAssets()
    {
        if (_cachedUnits != null)
        {
            foreach (GameObject unit in _cachedUnits.Values)
            {
                Addressables.Release(unit);
            }

            _cachedUnits.Clear();
        }

        if (_cachedUnitsData != null)
        {
            foreach (UnitData unitData in _cachedUnitsData.Values)
            {
                Addressables.Release(unitData);
            }

            _cachedUnitsData.Clear();
        }
    }
}

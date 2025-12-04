using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UnitFactory
{
    private Dictionary<string, GameObject> _cachedUnits = new Dictionary<string, GameObject>();

    public async UniTask<GameObject> CreateUnit(UnitType type, Waypoint start, Vector2 position)
    {
        GameObject unit = Object.Instantiate(await LoadUnit(type), position, Quaternion.identity);

        if (!unit.TryGetComponent<UnitController>(out UnitController controller))
        {
            Debug.LogError($"Unit Controller on {unit.name} not found");
            Object.Destroy(unit);
            return null;
        }

        controller.Init(start);

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
}

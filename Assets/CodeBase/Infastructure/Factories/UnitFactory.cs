using Cysharp.Threading.Tasks;
using UnityEngine;

public class UnitFactory : IUnitFactory
{
    private readonly IAssetProvider _assetProvider;
    private readonly DataCatalog _dataCatalog;

    public UnitFactory(IAssetProvider assetProvider, DataCatalog dataCatalog)
    {
        _assetProvider = assetProvider;
        _dataCatalog = dataCatalog;
    }

    public async UniTask<GameObject> CreateUnit(UnitType type, Waypoint start, Vector2 position)
    {
        EnemyUnitData unitData = _dataCatalog.GetUnitData(type);

        if (unitData == null)
        {
            Debug.LogError($"Failed to create unit, {type} data is null");
            return null;
        }

        GameObject unit = Object.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(unitData.PrefabReference), position, Quaternion.identity);

        if (unit == null)
        {
            Debug.LogError($"Failed to create unit, {type} prefab is null");
            return null;
        }

        unit.GetComponent<EnemyUnitController>().Init(unitData, start);

        return unit;

    }
}

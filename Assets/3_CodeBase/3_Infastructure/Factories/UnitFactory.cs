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
        UnitData unitData = _dataCatalog.GetUnitData(type);

        GameObject unit = Object.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(unitData.PrefabReference), position, Quaternion.identity);

        unit.GetComponent<UnitController>().Init(unitData, start);

        return unit;

    }
}

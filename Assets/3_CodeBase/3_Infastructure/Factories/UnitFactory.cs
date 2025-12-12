using Cysharp.Threading.Tasks;
using UnityEngine;

public class UnitFactory : IUnitFactory
{
    private readonly IAssetProvider _assetProvider;

    public UnitFactory(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public async UniTask<GameObject> CreateUnit(UnitType type, Waypoint start, Vector2 position)
    {
        UnitData unitData = await _assetProvider.LoadAssetData<UnitData>(type.ToString());

        GameObject unit = Object.Instantiate(await _assetProvider.LoadAsset(type.ToString()), position, Quaternion.identity);

        unit.GetComponent<UnitController>().Init(unitData, start);

        return unit;

    }
}

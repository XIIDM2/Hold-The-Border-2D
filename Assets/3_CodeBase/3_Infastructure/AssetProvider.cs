using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetProvider : IAssetProvider
{ 
    private readonly Dictionary<string, ScriptableObject> _cachedData = new Dictionary<string, ScriptableObject>();
    private readonly Dictionary<string, GameObject> _cachedPrefabs = new Dictionary<string, GameObject>();

    private readonly Dictionary<string, AsyncOperationHandle> _asyncOperations = new Dictionary<string, AsyncOperationHandle>();

    public async UniTask<GameObject> LoadAsset(string label)
    {
        if (_cachedPrefabs.TryGetValue(label, out GameObject cached))
        {
            return cached;
        }

        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(label);
        await handle.ToUniTask();

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Failed to download asset from addressables");
            Addressables.Release(handle);
            return null;
        }

        _asyncOperations[label] = handle;
        _cachedPrefabs[label] = handle.Result;

        return handle.Result;

    }

    public async UniTask<T> LoadAssetData<T>(string label) where T : ScriptableObject
    {
        if (_cachedData.TryGetValue(label, out ScriptableObject cached))
        {
            return cached as T;
        }

        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(label);
        await handle.ToUniTask();

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Failed to download asset data from addressables");
            Addressables.Release(handle);
            return null;
        }

        _asyncOperations[label] = handle;
        _cachedData[label] = handle.Result;

        return handle.Result;
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }

    public void ReleaseAll()
    {
        foreach (AsyncOperationHandle handle in _asyncOperations.Values)
        {
            Addressables.Release(handle);
        }

        _cachedData.Clear();
        _cachedPrefabs.Clear();
        _asyncOperations.Clear();
    }
}

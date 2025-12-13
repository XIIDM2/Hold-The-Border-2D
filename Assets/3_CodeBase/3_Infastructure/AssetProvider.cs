using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetProvider : IAssetProvider
{ 

    private readonly Dictionary<string, AsyncOperationHandle> _asyncOperations = new Dictionary<string, AsyncOperationHandle>();

    public async UniTask<T> LoadAssetByReference<T>(AssetReference reference) where T : class
    {
        string GUID = reference.AssetGUID;

        if (_asyncOperations.TryGetValue(GUID, out AsyncOperationHandle cachedHandle))
        {
            return cachedHandle.Result as T;
        }

        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(GUID);

        await handle.ToUniTask();

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Failed to download asset from addressables");
            Addressables.Release(handle);
            return null;
        }

        _asyncOperations[GUID] = handle;
        return handle.Result;
    }

    public void Release(AssetReference reference)
    {
        string GUID = reference.AssetGUID;

        if (_asyncOperations.TryGetValue(GUID, out var handle))
        {
            Addressables.Release(handle);
            _asyncOperations.Remove(GUID);
        }
    }

    public void ReleaseAll()
    {
        foreach (AsyncOperationHandle handle in _asyncOperations.Values)
        {
            Addressables.Release(handle);
        }

        _asyncOperations.Clear();
    }
}

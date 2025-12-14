using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class AssetProvider : IAssetProvider
{ 

    private readonly Dictionary<string, AsyncOperationHandle> _asyncOperations = new Dictionary<string, AsyncOperationHandle>();
    private readonly Dictionary<string, string> _addressDictionary = new Dictionary<string, string>();

    public async UniTask LoadMultipleAssetsByLabel<T>(string label) where T : class
    {
        var loadResourceLocationsHandle = Addressables.LoadResourceLocationsAsync(label);

        await loadResourceLocationsHandle.ToUniTask();

        foreach (IResourceLocation resourceLocation in loadResourceLocationsHandle.Result)
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(resourceLocation);

            await handle.ToUniTask();

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Failed to download asset from {resourceLocation}");
                Addressables.Release(handle);
                continue;
            }

            if (!_asyncOperations.TryAdd(resourceLocation.PrimaryKey, handle))
            {
                Addressables.Release(handle);
            }
        }

        Addressables.Release(loadResourceLocationsHandle);
    }

    public async UniTask<T> LoadAssetByReference<T>(AssetReference reference) where T : class
    {
        string address = await LoadAssetAdress(reference);

        if (_asyncOperations.TryGetValue(address, out AsyncOperationHandle cachedHandle))
        {
            return cachedHandle.Result as T;
        }

        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

        await handle.ToUniTask();

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to download asset from {reference} adress");
            Addressables.Release(handle);
            return null;
        }

        if (!_asyncOperations.TryAdd(address, handle))
        {
            Addressables.Release(handle);
        }
             
        return handle.Result;
    }

    public async UniTask Release(AssetReference reference)
    {
        string address = await LoadAssetAdress(reference);

        if (_asyncOperations.TryGetValue(address, out var handle))
        {
            Addressables.Release(handle);
            _asyncOperations.Remove(address);
        }
    }

    public void ReleaseAll()
    {
        foreach (AsyncOperationHandle handle in _asyncOperations.Values)
        {
            Addressables.Release(handle);
        }

        _asyncOperations.Clear();
        _addressDictionary.Clear();
    }

    private async UniTask<string> LoadAssetAdress(AssetReference assetReference)
    {
        if (_addressDictionary.TryGetValue(assetReference.AssetGUID, out string cachedAdress))
        {
            return cachedAdress;
        }

        var opHandle = Addressables.LoadResourceLocationsAsync(assetReference);

        await opHandle.ToUniTask();

        if (opHandle.Status != AsyncOperationStatus.Succeeded ||
            opHandle.Result == null ||
            opHandle.Result.Count == 0)
        {
            Debug.LogError($"Failed to download {assetReference} address from addressables");
            Addressables.Release(opHandle);
            return null;
        }

        string address = opHandle.Result[0].PrimaryKey;

        _addressDictionary.TryAdd(assetReference.AssetGUID, address);

        Addressables.Release(opHandle);

        return address;
    }
}

using Core.Interfaces;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Assets.CodeBase.Infastructure.Services
{
    public class AssetProviderService : IAssetProviderService
    {

        private readonly Dictionary<string, AsyncOperationHandle> _asyncOperationsDict = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, string> _addressDictionary = new Dictionary<string, string>();

        public async UniTask LoadMultipleAssetsByLabel(string label, CancellationToken cancellationToken)
        {
            var loadResourceLocationsHandle = Addressables.LoadResourceLocationsAsync(label);

            await loadResourceLocationsHandle.ToUniTask(cancellationToken: cancellationToken);

            List<UniTask> tasks = new List<UniTask>();
            Dictionary<string, AsyncOperationHandle> currentAsyncOperationsDict = new Dictionary<string, AsyncOperationHandle>();

            foreach (IResourceLocation resourceLocation in loadResourceLocationsHandle.Result)
            {
                AsyncOperationHandle handle = Addressables.LoadAssetAsync<Object>(resourceLocation);

                if (!_asyncOperationsDict.TryAdd(resourceLocation.PrimaryKey, handle))
                {
                    Addressables.Release(handle);
                    continue; 
                }
                currentAsyncOperationsDict.Add(resourceLocation.PrimaryKey, handle);
                tasks.Add(handle.ToUniTask());

            }

            Addressables.Release(loadResourceLocationsHandle);

            try
            {
                await UniTask.WhenAll(tasks);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to download asset with following exception: " + ex);
            }


            foreach (KeyValuePair<string, AsyncOperationHandle> pair in currentAsyncOperationsDict)
            {
                if (pair.Value.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError($"Failed to download asset from {pair.Key}");
                    Addressables.Release(pair.Value);
                    _asyncOperationsDict.Remove(pair.Key);
                    continue;
                }          
            }
        }

        public async UniTask<T> LoadAssetByReference<T>(AssetReference reference, CancellationToken cancellationToken) where T : class
        {
            string address = await LoadAssetAddress(reference, cancellationToken);

            if (_asyncOperationsDict.TryGetValue(address, out AsyncOperationHandle cachedHandle))
            {
                return cachedHandle.Result as T;
            }

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

            await handle.ToUniTask(cancellationToken : cancellationToken);

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Failed to download asset from {reference} adress");
                Addressables.Release(handle);
                return default;
            }

            if (!_asyncOperationsDict.TryAdd(address, handle))
            {
                Addressables.Release(handle);
            }

            return handle.Result;
        }

        public async UniTask ReleaseAsset(AssetReference reference)
        {
            string address = await LoadAssetAddress(reference);

            if (_asyncOperationsDict.TryGetValue(address, out var handle))
            {
                Addressables.Release(handle);
                _asyncOperationsDict.Remove(address);
            }
        }

        public void ReleaseAllAssets()
        {
            foreach (AsyncOperationHandle handle in _asyncOperationsDict.Values)
            {
                Addressables.Release(handle);
            }

            _asyncOperationsDict.Clear();
            _addressDictionary.Clear();
        }

        private async UniTask<string> LoadAssetAddress(AssetReference assetReference, CancellationToken cancellationToken=default)
        {
            if (_addressDictionary.TryGetValue(assetReference.AssetGUID, out string cachedAdress))
            {
                return cachedAdress;
            }

            var opHandle = Addressables.LoadResourceLocationsAsync(assetReference);

            await opHandle.ToUniTask(cancellationToken : cancellationToken);

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
}
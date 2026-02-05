using Core.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Factories
{
    public abstract class BaseFactory
    {
        protected readonly IAssetProviderService _assetProvider;
        protected readonly IObjectResolver _objectResolver;

        public BaseFactory(IAssetProviderService assetProvider, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _objectResolver = objectResolver;
        }

        public async UniTask<T> Create<T>(AssetReference reference, Vector2 position) where T : class 
        {
            GameObject prefab = _objectResolver.Instantiate(await _assetProvider.LoadAssetByReference<GameObject>(reference), position, Quaternion.identity);

            if (prefab == null)
            {
                Debug.LogError($"Failed to download prefab for {typeof(T)} from Addressables");
            }

            if (!prefab.TryGetComponent<T>(out T component))
            {
                Debug.LogError($"Failed to get component {typeof(T)} from gameObject");
                Object.Destroy(prefab);
                return default;
            }

            return component;
        }
    }
}
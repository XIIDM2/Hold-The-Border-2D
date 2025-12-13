using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public interface IAssetProvider
{
    UniTask<T> LoadAssetByReference<T>(AssetReference reference) where T : class;
    void Release(AssetReference reference);
    void ReleaseAll();
}

using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Core.Interfaces
{
    public interface IAssetProviderService
    {
        UniTask LoadMultipleAssetsByLabel<T>(string label) where T : class;
        UniTask<T> LoadAssetByReference<T>(AssetReference reference) where T : class;
        UniTask ReleaseAsset(AssetReference reference);
        void ReleaseAllAssets();
    }
}
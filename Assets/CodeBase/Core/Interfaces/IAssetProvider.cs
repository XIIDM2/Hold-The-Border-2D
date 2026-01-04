using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Core.Interfaces
{
    public interface IAssetProvider
    {
        UniTask LoadMultipleAssetsByLabel<T>(string label) where T : class;
        UniTask<T> LoadAssetByReference<T>(AssetReference reference) where T : class;
        UniTask Release(AssetReference reference);
        void ReleaseAll();
    }
}
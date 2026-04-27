using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.AddressableAssets;

namespace Core.Interfaces
{
    public interface IAssetProviderService
    {
        UniTask LoadMultipleAssetsByLabel(string label, CancellationToken cancellationToken);
        UniTask<T> LoadAssetByReference<T>(AssetReference reference, CancellationToken cancellationToken) where T : class;
        UniTask ReleaseAsset(AssetReference reference);
        void ReleaseAllAssets();
    }
}
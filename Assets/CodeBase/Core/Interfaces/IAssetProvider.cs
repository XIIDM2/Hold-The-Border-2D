using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.AddressableAssets;

namespace Core.Interfaces
{
    public interface IAssetProvider
    {
        UniTask LoadMultipleAssetsByLabel<T>(string label, CancellationToken cancellationToken) where T : class;
        UniTask<T> LoadAssetByReference<T>(AssetReference reference, CancellationToken cancellationToken) where T : class;
        UniTask Release(AssetReference reference);
        void ReleaseAll();
    }
}
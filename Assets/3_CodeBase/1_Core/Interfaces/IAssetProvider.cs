using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IAssetProvider
{
    UniTask<T> LoadAssetData<T>(string label) where T : ScriptableObject;
    UniTask<GameObject> LoadAsset(string label);
    void Release();
    void ReleaseAll();
}

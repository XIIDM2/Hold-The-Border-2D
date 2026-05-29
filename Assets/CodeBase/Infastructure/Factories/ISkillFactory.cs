using Cysharp.Threading.Tasks;
using Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Factories
{
    public interface ISkillFactory
    {
        UniTask<TSkill> CreateSkillGameObject<TSkill>(AssetReferenceGameObject assetReference, Vector2 position) where TSkill : Component, ISkill;
    }
}
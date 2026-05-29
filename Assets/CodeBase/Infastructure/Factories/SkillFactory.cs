using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Infrastructure.Factories
{
    public class SkillFactory : BaseFactory, ISkillFactory
    {
        private CancellationToken _ctc;
        public SkillFactory(IAssetProviderService assetProvider, IObjectResolver objectResolver, CancellationToken ctc) : base(assetProvider, objectResolver)
        {
            _ctc = ctc;
        }

        public async UniTask<TSkill>CreateSkillGameObject<TSkill>(AssetReferenceGameObject assetReference, Vector2 position) where TSkill : Component, ISkill
        {
            TSkill skill = await Create<TSkill>(assetReference, position, _ctc);
            return skill;
        }
    }
}
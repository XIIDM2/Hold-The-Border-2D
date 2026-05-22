using Core.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Infrastructure.Factories
{
    public class SkillFactory : BaseFactory, ISkillFactory
    {
        public SkillFactory(IAssetProviderService assetProvider, IObjectResolver objectResolver) : base(assetProvider, objectResolver)
        {
        }

        public async UniTask<GameObject> CreateSkillGameObject()
        {
            // TODO
            return null;
        }
    }
}
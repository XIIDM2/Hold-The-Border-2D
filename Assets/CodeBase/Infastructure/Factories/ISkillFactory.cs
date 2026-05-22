using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface ISkillFactory
    {
        UniTask<GameObject> CreateSkillGameObject();
    }
}
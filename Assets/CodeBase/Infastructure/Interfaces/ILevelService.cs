using UnityEngine.Events;

namespace Infrastructure.Services
{
    public interface ILevelService
    {
        event UnityAction Victory;
        event UnityAction Defeat;
        void Init();
        void StartWaves();
    }
}
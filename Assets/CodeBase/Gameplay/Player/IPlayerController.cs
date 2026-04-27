using Infrastructure.Interfaces;
using UnityEngine.Events;

namespace Gameplay.Player
{
    public interface IPlayerController
    {
        event UnityAction<int> GoldChanged;
        IDamageable Health { get; }
        int Gold { get; }
        int SkipWaveTimerGoldMultiplier { get; }
        bool TrySpendGold(int amount);
        void AddGold(int amount);

    }
}
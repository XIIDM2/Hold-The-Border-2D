using Infrastructure.Interfaces;
using UnityEngine.Events;

namespace Gameplay.Player
{
    public interface IPlayerController
    {
        IDamageable Health { get; }
        int Gold { get; }

        event UnityAction<int> GoldChanged;
        public bool TrySpendGold(int amount);
        public void AddGold(int amount);

    }
}
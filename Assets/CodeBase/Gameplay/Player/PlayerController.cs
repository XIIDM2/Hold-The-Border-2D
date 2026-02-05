using Data;
using Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Player
{
    public class PlayerController : IPlayerController
    {
        public event UnityAction<int> GoldChanged;
        public IDamageable Health { get; private set; }
        public int Gold { get; private set; }

        public PlayerController(PlayerData _data)
        {
            Health = new Health();
            Health.Initialize(_data.MaxHeath);
            Gold = _data.StartGold;
        }

        public bool TrySpendGold(int amount)
        {
            if (Gold < amount)
            {
                Debug.LogWarning("Not Enough Gold");
                return false;
            }

            Gold -= amount;
            GoldChanged?.Invoke(Gold);
            return true;

        }

        public void AddGold(int amount)
        {
            Gold += amount;
        }

    }
}
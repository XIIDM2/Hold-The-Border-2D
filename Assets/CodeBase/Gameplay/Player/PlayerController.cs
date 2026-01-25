using Data;
using Infrastructure.Interfaces;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : IPlayerController
{
    public event UnityAction<int> OnGoldChanged;
    public IDamageable Health {  get; private set; }
    public int Gold { get; private set; }

    public PlayerController(PlayerData _data)
    {
        Health = new Health();
        Health.Init(_data.MaxHeath);
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
        OnGoldChanged?.Invoke(Gold);
        return true;

    }

    public void GetGold(int amount)
    {
        Gold += amount;
    }

}

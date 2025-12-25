using System;
using UnityEngine;

public class PlayerController : IPlayerController
{
    public Health Health {  get; private set; }
    public int Gold { get; private set; }

    public void Init(PlayerData _data)
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
        return true;

    }

    public void GetGold(int amount)
    {
        Gold += amount;
    }

}

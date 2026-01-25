using Data;
using Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public interface IPlayerController
{
    IDamageable Health { get; }
    int Gold { get; }

    event UnityAction<int> OnGoldChanged;
    public bool TrySpendGold(int amount);
    public void GetGold(int amount);

}

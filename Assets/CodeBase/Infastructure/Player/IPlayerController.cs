using UnityEngine;

public interface IPlayerController
{
    IDamageable Health { get; }
    int Gold { get; }
    public void Init(PlayerData _data);
    public bool TrySpendGold(int amount);
    public void GetGold(int amount);

}

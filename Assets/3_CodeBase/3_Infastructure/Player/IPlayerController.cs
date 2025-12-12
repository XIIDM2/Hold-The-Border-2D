using UnityEngine;

public interface IPlayerController
{
    IHealth Health { get; }
    int Gold { get; }
    public void Init(PlayerData _data);
    public bool TrySpendGold(int amount);
    public void GetGold(int amount);

}

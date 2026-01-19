using Gameplay.Towers;
using UnityEngine;


public class TowerUpgradeButton : MonoBehaviour
{
    [SerializeField] private TowerController _controller;

    public void Upgrade()
    {
        Messenger<TowerController>.Broadcast(Events.TowerUpgradeRequested, _controller);    
    }
}


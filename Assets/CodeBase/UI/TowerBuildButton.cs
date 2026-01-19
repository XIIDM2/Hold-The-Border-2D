using Gameplay.Towers;
using UnityEngine;

public class TowerBuildButton : MonoBehaviour
{
    [SerializeField] private TowerType _type;

    public void Build()
    {
        Messenger<TowerType, Vector2>.Broadcast(Events.TowerBuildRequested, _type, Vector2.zero);
    }

   
}

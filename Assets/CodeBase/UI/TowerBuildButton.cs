using Gameplay.Towers;
using UnityEngine;

public class TowerBuildButton : MonoBehaviour
{
    [SerializeField] private TowerType _type;

    public void Build()
    {
        Debug.Log("Button Clicked");
        Messenger<TowerType, Vector2>.Broadcast(Events.TowerBuildRequested, _type, new Vector2(3.5f, -7f));
    }
}

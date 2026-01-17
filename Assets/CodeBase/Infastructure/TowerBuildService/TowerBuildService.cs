using Gameplay.Towers;
using Infrastructure.Factories;
using UnityEngine;
using VContainer;

public class TowerBuildService : MonoBehaviour, ITowerBuildService
{
    private IPlayerController _player;
    private ITowerFactory _factory;

    [Inject]
    public void Construct(IPlayerController player, ITowerFactory factory)
    {
        _player = player;
        _factory = factory;
    }

    private void OnEnable()
    {
        Messenger<TowerType, Vector2>.AddListener(Events.TowerBuildRequested, Build);
    }

    private void OnDisable()
    {
        Messenger<TowerType, Vector2>.RemoveListener(Events.TowerBuildRequested, Build);
    }


    public async void Build(TowerType type, Vector2 position)
    {
        await _factory.CreateTower(type, position);
    }

    // upgradetower
}

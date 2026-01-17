using Gameplay.Towers;
using UnityEngine;


public interface ITowerBuildService
{
    public void Build(TowerType type, Vector2 position);

}
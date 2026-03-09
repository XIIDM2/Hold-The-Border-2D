using Gameplay.Towers;

namespace Infrastructure.Services
{
    public interface IRadiusVisualizerService
    {
        void ShowVisualizer(TowerController tower, float radius);

        void HideVisualizer();
    }
}
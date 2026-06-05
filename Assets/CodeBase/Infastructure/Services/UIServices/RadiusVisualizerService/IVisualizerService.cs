using UnityEngine;

namespace Infrastructure.Services
{
    public interface IVisualizerService
    {
        void SetVisualizerPosition(Vector2 position);
        void SetVisualizerHologram(Sprite hologram);
        void SetVisualizerRadius(float radius);
        void ShowVisualizer(Vector2 position, float radius);
        void ShowVisualizer();
        void HideVisualizer();
    }
}
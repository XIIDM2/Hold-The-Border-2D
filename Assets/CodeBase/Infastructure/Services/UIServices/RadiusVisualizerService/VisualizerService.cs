using Gameplay.UI;
using UnityEngine;

namespace Infrastructure.Services
{
    public class VisualizerService : MonoBehaviour, IVisualizerService
    {
        [SerializeField] private Visualizer _visualizer;

        private void Start()
        {
            _visualizer = Instantiate(_visualizer);
            HideVisualizer();
        }

        public void SetVisualizerRadius(float radius)
        {
            _visualizer.SetRadius(radius);
        }

        public void SetVisualizerHologram(Sprite hologram)
        {
            _visualizer.SetHologram(hologram);
        }
        public void SetVisualizerPosition(Vector2 position)
        {
            _visualizer.SetPosition(position);
        }

        public void ShowVisualizer()
        {
            _visualizer.gameObject.SetActive(true);
        }

        public void ShowVisualizer(Vector2 position, float radius)
        {
            SetVisualizerRadius(radius);
            SetVisualizerPosition(position);
            ShowVisualizer();
        }

        public void HideVisualizer()
        {
            if (_visualizer)
            {
                _visualizer.CleanUp();
                _visualizer.gameObject.SetActive(false);
            }
        }

    }
}
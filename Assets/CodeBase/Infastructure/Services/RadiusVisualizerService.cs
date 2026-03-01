using Gameplay.Towers;
using UnityEngine;

namespace Infrastructure.Services
{
    public class RadiusVisualizerService : MonoBehaviour
    {
        [SerializeField] private GameObject _radiusVisualizerPrefab;

        private GameObject _radiusVisualizer;

        public void ShowVisualizer(TowerController tower, float radius=1f)
        {
            HideVisualizer();
            _radiusVisualizer = Instantiate(_radiusVisualizerPrefab, tower.transform.position, Quaternion.identity, tower.transform);
            _radiusVisualizer.transform.localScale = 2f * radius * Vector3.one;
        }

        public void HideVisualizer()
        {
            if (_radiusVisualizer) Destroy(_radiusVisualizer);
        }
    }
}
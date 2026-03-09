using Gameplay.Towers;
using UnityEngine;

namespace Infrastructure.Services
{
    public class RadiusVisualizerService : MonoBehaviour, IRadiusVisualizerService
    {
        [SerializeField] private GameObject _radiusVisualizerPrefab;

        private GameObject _radiusVisualizer;

        private void Start()
        {
            _radiusVisualizer = Instantiate(_radiusVisualizerPrefab);
            HideVisualizer();
        }

        public void ShowVisualizer(TowerController tower, float radius=1f)
        {
            HideVisualizer();

            _radiusVisualizer.transform.position =  tower.transform.position;
            _radiusVisualizer.transform.SetParent(tower.transform);
            _radiusVisualizer.transform.localScale = 2f * radius * Vector3.one;

            _radiusVisualizer.SetActive(true);

        }

        public void HideVisualizer()
        {
            if (_radiusVisualizer) _radiusVisualizer.SetActive(false);
        }
    }
}
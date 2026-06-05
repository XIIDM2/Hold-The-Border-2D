using UnityEngine;

namespace Gameplay.UI
{
    public class Visualizer : MonoBehaviour
    {
        [SerializeField] private GameObject _radius;
        [SerializeField] private SpriteRenderer _hologram;

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetRadius(float radius)
        {
            _radius.transform.localScale = 2f * radius * Vector3.one;
        }

        public void SetHologram(Sprite hologram)
        {
            _hologram.sprite = hologram;
        }

        public void CleanUp()
        {
            _hologram.sprite = null;
        }
    }
}
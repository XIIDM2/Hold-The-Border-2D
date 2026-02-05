using DG.Tweening;
using UnityEngine;

namespace Gameplay.Towers.Units
{
    public class TowerUnitSpawn : MonoBehaviour
    {
        [SerializeField] private float _spawnDuration = 1.0f;

        private TowerUnitAnimation _unitAnimation;
        private SpriteRenderer _sprite;

        private void Awake()
        {
            _unitAnimation = GetComponent<TowerUnitAnimation>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void OnEnable() => _unitAnimation.TowerUnitSpawn += SpawnUnitAnimation;
        private void OnDisable() => _unitAnimation.TowerUnitSpawn -= SpawnUnitAnimation;

        private void Start()
        {
            _unitAnimation.enabled = false;

            Color color = _sprite.color;
            color.a = 0.0f;
            _sprite.color = color;
        }

        private void SpawnUnitAnimation()
        {
            _sprite.DOFade(1.0f, _spawnDuration).OnComplete(() =>
            {
                _unitAnimation.enabled = true;
            }).SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

    }
}
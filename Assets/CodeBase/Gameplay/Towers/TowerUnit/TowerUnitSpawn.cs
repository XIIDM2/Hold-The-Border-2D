using DG.Tweening;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Towers.Units
{
    public class TowerUnitSpawn : MonoBehaviour
    {
        [SerializeField] private float _spawnDuration = 1.0f;

        private TowerUnitAnimation _unitAnimation;
        private SpriteRenderer _unitSprite;

        private Tween _unitSpawnTween;

        private void Awake()
        {
            _unitAnimation = GetComponent<TowerUnitAnimation>();
            _unitSprite = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _unitAnimation.TowerUnitSpawn += SpawnUnitAnimation;
        }

        private void Start()
        {
            _unitAnimation.enabled = false;

            Color color = _unitSprite.color;
            color.a = 0.0f;
            _unitSprite.color = color;
        }
        private void OnDisable()
        {
            _unitAnimation.TowerUnitSpawn -= SpawnUnitAnimation;
            if (_unitSpawnTween != null) DOTween.Kill(_unitSpawnTween);
        }

        private void SpawnUnitAnimation()
        {
            _unitSpawnTween = _unitSprite.DOFade(1.0f, _spawnDuration).OnComplete(() =>
            {
                _unitAnimation.enabled = true;


            }).SetLink(gameObject);
        }

    }
}
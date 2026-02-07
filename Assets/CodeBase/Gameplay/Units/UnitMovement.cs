using Core.Utilities;
using Core.Utilities.CustomProperties;
using UnityEngine;

namespace Gameplay.Units
{
    public class UnitMovement : MonoBehaviour
    {
        public Vector2 Direction { get; private set; }

        [SerializeField, ReadOnly] private float _movementSpeed;

        private Rigidbody2D _rb;
        private SpriteRenderer _sprite;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
        }

        public void Init(float movementSpeed)
        {
            _movementSpeed = movementSpeed;
        }

        public void Move(Vector2 target)
        {
            Direction = target - _rb.position;

            _rb.MovePosition(_rb.position + _movementSpeed * Time.fixedDeltaTime * Direction.normalized);

            Utilities.FlipSprite(_sprite, Direction.x);
        }
    }
}
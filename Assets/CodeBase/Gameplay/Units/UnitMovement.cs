using Core.Utilities.CustomProperties;
using UnityEngine;

namespace Gameplay.Units
{
    public class UnitMovement : MonoBehaviour
    {
        public Vector2 Direction { get; private set; }

        [SerializeField, ReadOnly] private float _movementSpeed;

        private Rigidbody2D _rb;

        private ObjectDirection _unitDirection;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _unitDirection = new ObjectDirection();
        }

        public void Init(float movementSpeed)
        {
            _movementSpeed = movementSpeed;
        }

        public void Move(Vector2 target)
        {
            Direction = target - _rb.position;

            _rb.MovePosition(_rb.position + Direction.normalized * Time.fixedDeltaTime * _movementSpeed);

            _unitDirection.FaceDirection(transform, Direction.x);
        }

    }
}
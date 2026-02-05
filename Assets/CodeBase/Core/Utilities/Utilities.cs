using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Core.Utilities
{
    public static class Utilities
    {
        public static float GetAngleFromVector(Vector2 direction)
        {
            direction = direction.normalized;

            float angle =  Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle < 0) angle += 360;

            return angle;
        }

        public static void FlipSprite(SpriteRenderer sprite, float value)
        {
            const float THRESHOLD = 0.001f;

            if (value < -THRESHOLD)
            {
                sprite.flipX = false;
            }
            else if (value > THRESHOLD)
            {
                sprite.flipX = true;
            }
        }

        public static void FlipTransform(Transform transform, float value)
        {
            const float THRESHOLD = 0.001f;
            Vector3 localScale = transform.localScale;

            if (value < -THRESHOLD)
            {
                localScale.x = Mathf.Abs(localScale.x);
            }
            else if (value > THRESHOLD)
            {
                localScale.x = -Mathf.Abs(localScale.x);
            }

            transform.localScale = localScale;
        }
    }
}
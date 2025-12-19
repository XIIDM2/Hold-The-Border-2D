using UnityEngine;

public class ObjectDirection
{
    private const float THRESHOLD = 0.001f;
    
    public void FaceDirection(Transform transform, float value)
    {
        Vector3 currentScale = transform.localScale;
        float newScaleX = currentScale.x;

        if (value < -THRESHOLD)
        {
            newScaleX = Mathf.Abs(newScaleX);
        }
        else if (value > THRESHOLD)
        {
            newScaleX = -Mathf.Abs(newScaleX);
        }

        if (currentScale.x != newScaleX)
        {
            currentScale.x = newScaleX;
            transform.localScale = currentScale;
        }
    }
}


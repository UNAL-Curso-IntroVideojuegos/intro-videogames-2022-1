using UnityEngine;

public static class VectorExtensions 
{
    public static Vector2 Rotate(this Vector2 target, float angle)
    {
        Vector2 rotatedVector = new Vector2(target.x, target.y);
        rotatedVector.x = target.x * Mathf.Cos(angle * Mathf.Deg2Rad) - target.y * Mathf.Sin(angle * Mathf.Deg2Rad);
        rotatedVector.y = target.x * Mathf.Sin(angle * Mathf.Deg2Rad) + target.y * Mathf.Cos(angle * Mathf.Deg2Rad);
        return rotatedVector;
    }

    public static Vector2 Normalize(this Vector2 target, float scale)
    {
        float magnitud = target.magnitude;
        if (magnitud == 0)
            return Vector2.zero;

        Vector2 point = new Vector2();
        point.x = scale * target.x / magnitud;
        point.y = scale * target.y / magnitud;

        return point;
    }
}

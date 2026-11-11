using UnityEngine;

public static class CircleCalc
{
    public static Vector3 PositionFromAngle(float radius, float angle)
    {
        return new Vector3

        {
            x = radius * Mathf.Sin(angle * Mathf.Deg2Rad),
            y = 0,
            z = radius * Mathf.Cos(angle * Mathf.Deg2Rad)
        };
    }
}

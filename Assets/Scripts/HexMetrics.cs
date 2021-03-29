using UnityEngine;



//=================================\\
//
//  Left at point 5
//  https://catlikecoding.com/unity/tutorials/hex-map/part-1/
//
//=================================\\

public static class HexMetrics
{
    public const float outerRadius = 10f;
    public const float innerRadius = outerRadius * 0.866025404f;

    private static Vector3[] corners =
    {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius)
    };

    public static Vector3 GetFirstCorner(HexDirection d)
    {
        return corners[(int)d];
    }

    public static Vector3 GetSecondCorner(HexDirection d)
    {
        return corners[(int)d + 1];
    }

}


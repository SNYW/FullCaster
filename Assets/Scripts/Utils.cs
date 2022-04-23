using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float GetDistance(Vector2 pos1, Vector2 pos2)
    {
        var dir = pos1 - pos2;
        return dir.sqrMagnitude;
    }

    public static float GetDistance(Vector3 pos1, Vector3 pos2)
    {
        var dir = pos1 - pos2;
        return dir.sqrMagnitude;
    }

    public static int RandomIndex<T>(List<T> list)
    {
        return Random.Range(0, list.Count);
    }

    public static T RandomFromList<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}

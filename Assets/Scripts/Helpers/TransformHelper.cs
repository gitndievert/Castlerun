using UnityEngine;

public static class TransformHelper
{

    /// <summary>
    /// Distance between two transforms in world space
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public static float DistanceCheck(Transform t1, Transform t2)
    {
        return Vector3.Distance(t1.position, t2.position);        
    }

    /// <summary>
    /// Returns if the distance between two tranforms is GREATER than the length specified
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static bool DistanceGreater(Transform t1, Transform t2, float amount)
    {
        float dist = DistanceCheck(t1, t2);
        return dist > amount;
    }

    /// <summary>
    /// Returns if the distance between two tranforms is LESS than the length specified 
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static bool DistanceLess(Transform t1, Transform t2, float amount)
    {
        float dist = DistanceCheck(t1, t2);
        return dist < amount;
    }

}

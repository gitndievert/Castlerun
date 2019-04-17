using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayHelpers
{
    /// <summary>
    /// Extention method that pulls a random chance on a fixed amount
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns>int</returns>
    public static int GetChance(this int amount, int min, int max)
    {
        if (amount > max) return max;
        int pick = Random.Range(min, max);
        return pick < amount ? pick : amount;
    }
       
    /// <summary>
    /// Shakes the camera (action quake)
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="duration"></param>
    /// <param name="intensity"></param>
    /// <returns></returns>
    public static IEnumerator ShakeCamera(Camera camera, float duration, float intensity)
    {
        Vector2 origPos = camera.transform.position;
        for (float t = 0.0f; t < duration; t += Time.deltaTime * intensity)
        {
            // Create a temporary vector2 with the camera's original position modified by a random distance from the origin.
            Vector2 tempVec = origPos + Random.insideUnitCircle;

            // Apply the temporary vector.
            camera.transform.position = tempVec;

            // Yield until next frame.
            yield return null;
        }

        // Return back to the original position.
        camera.transform.position = origPos;
    }
}

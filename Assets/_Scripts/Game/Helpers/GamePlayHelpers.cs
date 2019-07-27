// ********************************************************************
// CONFIDENTIAL - DO NOT DISTRIBUTE
// COPYRIGHT 2019-2020 Wacky Potato Games, LLC. All Rights Reserved.
// 
// If you send, receive, or use this file for any purpose other than
// internal use by Wacky Potato Games, it is without permission and an act of theft.
// Report any misuse of this file immediately to contact@wackypotato.com
// Misuse or failure to report misuse will subject you to legal action.
// 
// The intellectual and technical concepts contained herein are
// proprietary and are protected by trade secret and/or copyright law.
// Dissemination or reproduction of this material is forbidden.
// ********************************************************************

using System.Collections;
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
    public static IEnumerator ShakeCamera(this Camera camera, float duration, float intensity)
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

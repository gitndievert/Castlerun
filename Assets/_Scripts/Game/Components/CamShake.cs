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

public class CamShake : MonoBehaviour
{
    public const float DefaultDuration = 1f;
    public const float DefaultIntensity = 2f;

    private static CamShake _instance;    

    private void Awake()
    {
        _instance = this;       
    }

    private void OnDestroy()
    {
        StopAll();
    }

    private void OnApplicationQuit()
    {
        StopAll();
    }
    
    public static void StopAll()
    {
        _instance.StopAllCoroutines();
    }

    public static void Shake()
    {
        _instance.StartCoroutine(ShakeCamera(DefaultIntensity, DefaultDuration));
    }

    public static void Shake(float intensity)
    {
        _instance.StartCoroutine(ShakeCamera(intensity, DefaultDuration));
    }

    public static void Shake(float intensity, float duration)
    {
        _instance.StartCoroutine(ShakeCamera(intensity, duration));
    }

    private static IEnumerator ShakeCamera(float intensity, float duration)
    {
        Transform cam = Camera.main.transform;
        Vector2 origPos = cam.position;

        for (float t = 0.0f; t < duration; t += Time.deltaTime * intensity)
        {            
            Vector2 tempVec = origPos + Random.insideUnitCircle;            
            cam.position = tempVec;            
            yield return null;
        }
        
        //cam.position = origPos;
    }





}

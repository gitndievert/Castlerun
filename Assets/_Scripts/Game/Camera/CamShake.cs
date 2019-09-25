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

using SBK.Unity;
using System.Collections;
using UnityEngine;

public class CamShake : DSingle<CamShake>
{    
    public const float DefaultDuration = 1f;
    public const float DefaultIntensity = 2f;

    protected override void PAwake()
    {
        
    }

    protected override void PDestroy()
    {
        StopAll();
    }    
    
    public void StopAll()
    {
        StopAllCoroutines();
    }

    public void Shake()
    {
        StartCoroutine(ShakeCamera(DefaultIntensity, DefaultDuration));
    }
    
    public void Shake(float intensity)
    {
        StartCoroutine(ShakeCamera(intensity, DefaultDuration));
    }

    public void Shake(float intensity, float duration)
    {
        StartCoroutine(ShakeCamera(intensity, duration));
    }

    private IEnumerator ShakeCamera(float intensity, float duration)
    {
        Vector2 origPos = transform.localPosition;

        for (float t = 0.0f; t < duration; t += Time.deltaTime * intensity)
        {            
            Vector2 tempVec = origPos + Random.insideUnitCircle;            
            transform.localPosition = tempVec;            
            yield return null;
        }
        
        transform.localPosition = origPos;
    }    
}

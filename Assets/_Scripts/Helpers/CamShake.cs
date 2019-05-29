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
        
        cam.position = origPos;
    }





}

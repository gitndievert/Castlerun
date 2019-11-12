using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTest : MonoBehaviour
{
    public float TestDuration = 1f;
    public float TestIntensity = 1f;       

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log($"Testing Shake on Duration({TestDuration}) and Intensity({TestIntensity})");
            CamShake.Instance.Shake(TestIntensity, TestDuration);
        }        
    }
}

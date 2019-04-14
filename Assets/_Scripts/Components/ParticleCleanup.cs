using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCleanup : MonoBehaviour
{
    public float WaitTimeSeconds;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, WaitTimeSeconds);
    }   
}

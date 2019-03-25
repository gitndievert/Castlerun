using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePrefab : MonoBehaviour
{    
    protected Renderer rend;    
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        rend = GetComponent<Renderer>();
        gameObject.AddComponent(typeof(AudioSource));
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {

    }

    protected void TagPrefab(string tag)
    {
        transform.tag = tag;
    }    
}

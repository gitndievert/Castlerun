using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePrefab : MonoBehaviour
{
    public int Health;
    public AudioClip DestroySound;

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

    public virtual void SetHit(int amount)
    {
        if (Health - amount > 0)
        {
            Health -= amount;
        }
        else
        {
            if (DestroySound != null)
                SoundManager.PlaySoundOnGameObject(gameObject, DestroySound);
            Destroy(gameObject);
        }
    }
}

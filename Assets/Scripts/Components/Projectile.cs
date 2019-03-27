using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BasePrefab
{
    public AudioClip TravelSound;
    public AudioClip ImpactSound;
    public int Damage;   
    
    protected override void Awake()
    {
        base.Awake();        
        TagPrefab("Projectile");
    }
    
    protected override void Start()
    {
        PlayTravelSound();
    }
    
    private void OnDestroy()
    {
        
    }

    public int GetDamage()
    {
        return Damage;
    }

    private void PlayTravelSound()
    {
        if (TravelSound == null) return;
        audioSource.loop = true;
        audioSource.clip = TravelSound;
        audioSource.Play();
    }

    private void PlayHitSound()
    {
        if (ImpactSound == null) return;
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.PlayOneShot(ImpactSound);
    }

    private void OnCollisionEnter(Collision col)
    {
        PlayHitSound();        
        Destroy(gameObject);        
    }
}

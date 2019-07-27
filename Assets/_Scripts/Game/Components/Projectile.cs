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
        //audioSource.loop = true;
        //audioSource.clip = TravelSound;
        //audioSource.Play();
    }

    private void PlayHitSound()
    {
        if (ImpactSound == null) return;
        SoundManager.PlaySoundOnGameObject(gameObject, ImpactSound);
    }

    private void OnCollisionEnter(Collision col)
    {
        PlayHitSound();        
        Destroy(gameObject);        
    }
}

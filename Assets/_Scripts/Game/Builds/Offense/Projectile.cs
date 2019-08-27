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

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : BasePrefab
{
    public AudioClip[] FireSounds;
    public AudioClip TravelSound;
    public AudioClip ImpactSound;
    
    #region Damage Settings
    /// <summary>
    /// Minimum Damage Delt
    /// </summary>
    public int MinDamage;
    /// <summary>
    /// Maximum Damage Delt
    /// </summary>
    public int MaxDamage;   
    #endregion


    private void Start()
    {        
        TagPrefab("Projectile");
        SoundManager.PlaySound(FireSounds);        
    }

    private void OnDestroy()
    {
        
    }

    private void Update()
    {
        
    }

    private void PlayTravelSound()
    {
        //NEED TO COME BACK

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
        if(col.transform.tag == Global.ENEMY_TAG)
        {
            Damage.ApplyDamage(col, MinDamage, MaxDamage, true);
            //Tuck away, don't destroy            
            Destroy(gameObject);           
        }        
    }
}   
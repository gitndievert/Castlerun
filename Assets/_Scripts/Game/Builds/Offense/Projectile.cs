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
    [Range(10,500)]
    public int MinDamage;
    /// <summary>
    /// Maximum Damage Delt
    /// </summary>
    [Range(10, 500)]
    public int MaxDamage;   
    #endregion

    public string TargetTag { get; set; }

    public override string DisplayName => "Projectile";

    protected override void Start()
    {        
        TagPrefab("Projectile");
        gameObject.layer = Global.PROJECTILE_LAYER;
        SoundManager.PlaySound(FireSounds);
        Physics.IgnoreLayerCollision(Global.PROJECTILE_LAYER, Global.ARMY_LAYER, true);
        //Default destruction timer
        //Projectiles should not take longer than 10 seconds to hit a target
        Destroy(gameObject, 10f);
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
        //Only hit the target enemy        
        if (col.transform.tag != TargetTag) return;        
        PlayHitSound();
        var target = col.gameObject.GetComponent<BasePrefab>();
        target.SetHit(MinDamage, MaxDamage);
        //Tuck away, don't destroy            
        Destroy(gameObject);                           
    }
}   
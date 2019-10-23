﻿// ********************************************************************
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

using Photon.Pun;
using UnityEngine;

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

    public float Speed = 50f;
    #endregion
       

    public override string DisplayName => "Projectile";

    private ISelectable _target = null;

    protected override void Start()
    {
        //Make Projectiles rigid and trigger
        RigidBody.isKinematic = true;
        Collider.isTrigger = true;

        TagPrefab(Global.PROJECTILE_TAG);
        gameObject.layer = Global.PROJECTILE_LAYER;
        SoundManager.PlaySound(FireSounds);
        Physics.IgnoreLayerCollision(Global.PROJECTILE_LAYER, Global.ARMY_LAYER, true);
        //Default destruction timer
        //Projectiles should not take longer than 10 seconds to hit a target
        Destroy(gameObject, 10f);        
    }

    protected virtual void Update()
    {
        if(_target != null)
        {
            Transform t = _target.GameObject.transform;
            Vector3 rPos = t.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(rPos);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, .5f);
        }

        transform.Translate(0, 0, Speed * Time.deltaTime, Space.Self);
    }

    public void Seek(ISelectable target)
    {
        _target = target;
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

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag != _target.GameObject.tag) return;
        PlayHitSound();
        var target = col.gameObject.GetComponent<BasePrefab>();
        target.SetHit(MinDamage, MaxDamage);
        //Tuck away, don't destroy                    
        PhotonNetwork.Destroy(gameObject);
    }

    /*private void OnCollisionEnter(Collision col)
    {
        //Only hit the target enemy        
        if (col.transform.tag != TargetTag) return;        
        PlayHitSound();
        var target = col.gameObject.GetComponent<BasePrefab>();
        target.SetHit(MinDamage, MaxDamage);
        //Tuck away, don't destroy            
        Destroy(gameObject);                           
    }*/
}   
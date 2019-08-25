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



//NATE NOTE: I MIGHT NEED TO RETHINK THESE
//Moving a lot of projectile pieces to it's own class for ALL 
public class OffensiveBuild : Build
{      
    public GameObject Projectile;
    public GameObject FireEffect;
    public float ConstructionTime = 2f;


    public Transform FirePosition;

    [Space(5)]
    [HideInInspector]
    public bool IsFiring = false;

    [SerializeField]    
    private float _trackFireTimer;

    protected override float BuildTime => ConstructionTime;
    protected override ResourceType ResourceType => ResourceType.Metal;

    protected override void Start()
    {
        base.Start();
        if (FirePosition == null)
            throw new System.Exception("Prefab needs a fire spot");
    }
        
    void Update()
    {
        /*if (_trackFireTimer != FireRate && IsFiring)
        {
            CancelInvoke("FireProjectile");
            Fire();
        }*/

    }

    public override bool ConfirmPlacement()
    {
        base.ConfirmPlacement();
        if (Projectile != null)
        {
            Fire();
        }

        return true;
    }

    public void Fire()
    {
        /*_trackFireTimer = FireRate;
        InvokeRepeating("FireProjectile", FireTimer, FireRate * 2);*/
    }

    protected void FireProjectile()
    {
        /*IsFiring = true;
        var proj = Instantiate(Projectile, FirePosition.position, Quaternion.identity);
        proj.name = "Incoming";
        proj.GetComponent<Rigidbody>().velocity = FirePosition.TransformDirection(Vector3.forward * FirePower);        
        SoundManager.PlaySoundOnGameObject(gameObject, FireSounds);
        if (FireEffect != null)
        {
            Instantiate(FireEffect, FirePosition.position, Quaternion.identity);            
        }*/
        
    }

    protected static Vector3 RandomFire()
    {
        int random = Random.Range(0, 100);
        if (random >= 0 && random < 25)
            return Vector3.left;
        else if (random > 25 && random <= 50)
            return Vector3.right;
        else if (random > 50 && random <= 75)
            return Vector3.forward;
        else
            return Vector3.down;
    }

    public override bool SetResourceType(ResourceType type)
    {
        return type == ResourceType;
    }    
}

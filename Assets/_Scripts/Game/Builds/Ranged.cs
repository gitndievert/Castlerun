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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ranged<T> : Troop where T : BasePrefab
{
    [Header("Projectile Used")]
    public Projectile Projectile;
    [Header("Projectile Properties")]
    public float FirePower = 5.5f;
    public GameObject SpawnPoint;

    protected ISelectable SetTarget { get; set; }

    protected Projectile CreatedProjectile;

    protected Transform EnemyTarget;

    protected override void Awake()
    {
        base.Awake();
        if (Projectile == null)
            throw new System.Exception("Projectile Required");
    }

    protected override void Update()
    {
        base.Update();
        if (EnemyTarget != null)
        {
            if (Vector3.Distance(transform.position, EnemyTarget.position) < Global.CASTER_STRIKE_DIST)
            {
                Attack();
            }
        }
    }

    public override void Target(ISelectable target)
    {
        EnemyTarget = target.GameObject.transform;
        SetTarget = target;
    }

    public void Fire()
    {

        var project = Instantiate(Projectile, SpawnPoint.transform.position, Quaternion.identity);
        project.GetComponent<Rigidbody>().AddForce(SpawnPoint.transform.forward * FirePower);
        /*if (FireEffect != null)
        {
            Instantiate(FireEffect, FirePosition.position, Quaternion.identity);            
        }*/
    }

    /// <summary>
    /// Could use this for random firing on cannon/catapult
    /// </summary>
    /// <returns></returns>
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

}

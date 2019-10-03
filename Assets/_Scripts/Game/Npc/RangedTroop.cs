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
using UnityEngine;

public abstract class RangedTroop : Troop
{
    const float MAXIMUM_BO_POWER = 2000f;

    [Header("Projectile Used")]
    public Projectile Projectile;
    [Header("Projectile Properties")]
    [Range(0f, MAXIMUM_BO_POWER)]
    public float FirePower = 300f;
    public GameObject SpawnPoint;
    
    protected Projectile CreatedProjectile;    

    protected override void Awake()
    {
        base.Awake();
        if (Projectile == null)
            throw new System.Exception("Projectile Required");
    }

    protected override void Update()
    {        
        //Only attack if target is in distance
        if (EnemyTargetTransform != null)
        {            
            CanAttack = Vector3.Distance(transform.position, EnemyTargetTransform.position) < Global.CASTER_STRIKE_DIST;            
        }

        base.Update();
    }  

    public override void Fire()
    {
        if (EnemyTargetTransform == null) return;
        Debug.Log("Attacking " + EnemyTargetTransform.name.ToString() + "!");
        anim.Play("Attack");
        var enemypos = EnemyTargetTransform.transform.position;
        transform.LookAt(new Vector3(enemypos.x, transform.position.y,enemypos.z));        
        var project = Instantiate(Projectile, SpawnPoint.transform.position, Quaternion.identity);
        project.TargetTag = EnemyTargetTransform.tag;
        project.GetComponent<Rigidbody>().AddForce(SpawnPoint.transform.forward * FirePower);        
    }

}

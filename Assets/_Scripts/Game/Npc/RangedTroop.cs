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


using Photon.Pun;
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
    
    //Attack code
    public override void Fire()
    {
        Debug.Log("Attacking " + AttackTarget.DisplayName + "!");
        anim.Play("Attack");
        var enemypos = AttackTarget.GameObject.transform.position;
        transform.LookAt(new Vector3(enemypos.x, transform.position.y, enemypos.z));
                
        if (Global.DeveloperMode) {
            var project = Instantiate(Projectile, SpawnPoint.transform.position, Quaternion.identity);
            project.Seek(AttackTarget);
        }
        else
        {
            var project = PhotonNetwork.Instantiate(Projectile.gameObject.name, SpawnPoint.transform.position, Quaternion.identity);
            project.GetComponent<Projectile>().Seek(AttackTarget);
        }
    }

}

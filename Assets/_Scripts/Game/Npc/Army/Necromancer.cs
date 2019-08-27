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

public enum NecroSpawnType
{
    Skelly,
    Zombie
}

public class Necromancer : Troop
{
    /// <summary>
    /// Number of Skeletons You Can Spawn
    /// </summary>
    [Range(0,5)]    
    public int SkeletonSpawnLimit = 3;
    public GameObject Skelly;
    public GameObject Zombie;

    public GameObject NecroProjectile;

    private Transform _target;
    private ProjectileSource _pSource;

    public override string DisplayName => "Necromancer";

    protected override void Start()
    {
        base.Start();
        _pSource = GetComponent<ProjectileSource>();
    }

    protected override void Update()
    {
        base.Update();
        if(_target != null)
        {
            if (Vector3.Distance(transform.position, _target.position) < Global.CASTER_STRIKE_DIST)
            {
                Attack();
            }
        }
    }

    public override void Target(ISelectable target)
    {
        _target = target.GameObject.transform;
        _pSource.SetTarget = target;
    }

    public override void Attack()
    {
        MoveStop();
        nav.isStopped = true;
        Debug.Log("Attacking " + _target.name.ToString() + "!");
        anim.Play("Attack");
    }

    public override void StopAttack()
    {
        anim.Play("Grounded");
    }

    public void Spawn(NecroSpawnType spawnType)
    {
        switch(spawnType)
        {
            case NecroSpawnType.Skelly:
                //skelly
                break;
            case NecroSpawnType.Zombie:
                //zombie
                break;
        }
    }

   
}

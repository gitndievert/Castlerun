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

/// <summary>
/// Base Class for all Infantry Foot Soldiers
/// </summary>
public class Fighter : Troop
{
    [Header("Damage Setters for Fighters")]
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

    /// <summary>
    /// Distance between fighter and target
    /// </summary>
    protected virtual float StrikeDistance
    {
        get { return Global.STRIKE_DIST; }
    }

    public override string DisplayName => "Soldier";

    protected override void Update()
    {
        //Only attack if target is in distance
        if (EnemyTargetTransform != null)
        {
            CanAttack = Vector3.Distance(transform.position, EnemyTargetTransform.position) < StrikeDistance;
        }
        base.Update();
    }

    public override void Fire()
    {
        if (EnemyTargetTransform == null) return;
        Debug.Log("Attacking " + EnemyTargetTransform.name.ToString() + "!");
        anim.Play("Attack");
        var enemypos = EnemyTargetTransform.transform.position;
        transform.LookAt(new Vector3(enemypos.x, transform.position.y, enemypos.z));
        //EnemyTarget.SetHit(Hit)
        Damage.ApplyDamage(EnemyTarget, MinDamage, MaxDamage, true);
    }
}

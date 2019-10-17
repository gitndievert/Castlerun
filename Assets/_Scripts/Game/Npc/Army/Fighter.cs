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

    public override string DisplayName => "Soldier";

    public override void Fire()
    {        
        Debug.Log("Attacking " + AttackTarget.DisplayName + "!");
        anim.Play("Attack");        
        var enemypos = AttackTarget.GameObject.transform.position;
        transform.LookAt(new Vector3(enemypos.x, transform.position.y, enemypos.z));
        AttackTarget.SetHit(MinDamage, MaxDamage);
    }
}

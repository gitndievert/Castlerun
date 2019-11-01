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

using SBK.Unity;
using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : PSingle<CompanionManager>
{
    /// <summary>
    /// All Castles in Game
    /// </summary>
    [Tooltip("All the companion prefabs in the game")]
    public List<GameObject> CompanionList;

    protected override void PAwake()
    {
       
    }

    protected override void PDestroy()
    {

    }

    public GameObject GetCompanionByType(CompanionType companion)
    {
        foreach(var c in CompanionList)
        {
            var comp = c.GetComponent<Companion>();
            if (comp.CompanionType == companion)
                return c;
        }

        return null;
    }

}

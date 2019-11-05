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

using System.Collections.Generic;
using UnityEngine;
using SBK.Unity;

public class CastleManager : PSingle<CastleManager>
{
    /// <summary>
    /// All Castles in Game
    /// </summary>
    [Tooltip("All the castle prefabs in the game")]
    public List<GameObject> CastleList;


    protected override void PAwake()
    {

    }

    protected override void PDestroy()
    {

    }

    public GameObject GetCastle(CastleType castle)
    {
        foreach (var c in CastleList)
        {
            var cc = c.GetComponent<Castle>();
            if (cc.CastleType == castle)
                return c;
        }

        return null;
    }

    public GameObject GetCastle(string menucastlename)
    {
        switch (menucastlename.ToLower())
        {
            default:
            case "classic":
                return GetCastle(CastleType.Default);
            case "fod":
                return GetCastle(CastleType.FortressOfDoom);
            case "other":
                return GetCastle(CastleType.Citadel);
        }
    }    

}

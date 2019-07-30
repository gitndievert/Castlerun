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

public class Plans : MonoBehaviour
{
    [Header("Basic Build Models")]
    [Space(5)]
    public GameObject WoodWall;
    public GameObject WoodFloor;
    public GameObject WoodRamp;
    [Space(5)]
    public GameObject RockWall;
    public GameObject RockFloor;
    public GameObject RockRamp;
    [Space(5)]
    public GameObject MetalWall;
    public GameObject MetalFloor;
    public GameObject MetalRamp;

    [Header("Player Source Models")]
    [Space(5)]
    public GameObject ResourceDepot;
    public GameObject Barracks;
    public GameObject WizardSpire;
    public GameObject ArcherRange;


    [Header("Offensive Build Models")]
    [Space(5)]
    public GameObject Cannon;
    public GameObject Catapult;


    public GameObject GetPlans(ResourceType resource, string build)
    {        
        build = build.ToLower();
        switch (resource)
        {
            case ResourceType.Wood:
                if (build == "wall")
                    return WoodWall;
                if (build == "floor")
                    return WoodFloor;
                if (build == "ramp")
                    return WoodRamp;
                break;
            case ResourceType.Rock:
                if (build == "wall")
                    return RockWall;
                if (build == "floor")
                    return RockFloor;
                if (build == "ramp")
                    return RockRamp;
                break;
            case ResourceType.Metal:
                if (build == "wall")
                    return MetalWall;
                if (build == "floor")
                    return MetalFloor;
                if (build == "ramp")
                    return MetalRamp;
                break;

        }

        return null;
    }


}

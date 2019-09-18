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
using System;

[RequireComponent(typeof(PlacementController))]
public class BuildManager : DSingle<BuildManager>
{
    [Header("Basic Builds")]
    public GameObject BasicWall;
    public GameObject BasicFloor;
    public GameObject BasicRamp;

    [Header("Buildings")]
    public List<Build> Builds;

    public PlacementController Placements { get; private set; }

    public static ResourceType[] ResourceIndex = {
        ResourceType.Wood,
        ResourceType.Rock,
        ResourceType.Metal
    };

    protected override void PAwake()
    {
        Placements = GetComponent<PlacementController>();
    }

    protected override void PDestroy()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
        
    }

    public void SetStatusEnabled(BuildingLabelTypes type, bool status)
    {
        foreach(var build in Builds)
        {
            if(build.BuildingLabelType == type)
            {
                build.EnableFromBuilder = status;
            }
        }
    }
    
    public Build GetBuildByType(BuildingLabelTypes building)
    {        
        if (Builds.Count > 0)
        {
            foreach (var build in Builds)
            {
                if (build.BuildingLabelType == building) return build;                    
            }
        }

        return null;
    }

    public void LoadByString(string building)
    {
        var buildtype = (BuildingLabelTypes)Enum.Parse(typeof(BuildingLabelTypes), building, true);
        LoadByType(buildtype);
    }

    public void LoadByType(BuildingLabelTypes building)
    {        
        var build = GetBuildByType(building);
        if (build != null)
        {            
            Placements.LoadObject(build.GameObject, build.IsBasic, true);
        }        
    }

    public void LoadBasicWall()
    {
        Placements.LoadObject(BasicWall, true);
    }

    public void LoadBasicFloor()
    {
        Placements.LoadObject(BasicFloor, true);
    }

    public void LoadBasicRamp()
    {
        Placements.LoadObject(BasicRamp, true);
    }

  
}

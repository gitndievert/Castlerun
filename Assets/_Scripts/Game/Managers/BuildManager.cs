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
using UnityEngine.UI;

[RequireComponent(typeof(PlacementController))]
public class BuildManager : DSingle<BuildManager>
{
    [Header("Basic Builds")]
    public GameObject BasicWall;
    public GameObject BasicFloor;
    public GameObject BasicRamp;

    [Header("Buildings")]
    public List<Build> AvailableBuilds;    

    public PlacementController Placements { get; private set; }    

    public static ResourceType[] ResourceIndex = {
        ResourceType.Wood,
        ResourceType.Rock,
        ResourceType.Metal
    };

    public BuildUI BuildUI
    {
        get { return UIManager.Instance.BuildingUIPanel; }
    }
    
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
        RefreshBuilds();               
    }

    private void Update()
    {
        
        
    }

    public void RefreshSelections()
    {
        foreach (Transform trans in BuildUI.SelectionsPanel.transform)
        {
            var image = trans.GetComponent<Image>();
            image.sprite = null;
            var color = image.color;
            color.a = 0f;
            image.color = color;
            trans.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    public void RefreshBuilds()
    {
        int i = 0;
        int buildcount = AvailableBuilds.Count;

        RefreshSelections();

        foreach (Transform trans in BuildUI.SelectionsPanel.transform)
        {
            if (i >= buildcount) break;
            var build = AvailableBuilds[i];

            var image = trans.GetComponent<Image>();
            image.sprite = build.GetIcon();
            var color = image.color;

            if (!build.EnableFromBuilder)
            {                
                color.r = 61f;
                color.g = 61f;
                color.b = 61f;
                color.a = 0.5f;                
            }
            else
            {
                trans.GetComponent<Button>().onClick.AddListener(() => LoadByType(build.BuildingLabelType));
                color.a = 0.85f;
            }

            image.color = color;

            i++;
        }        
    }
    
    public void SetStatusEnabled(BuildingLabelTypes type, bool status)
    {
        foreach(var build in AvailableBuilds)
        {
            if(build.BuildingLabelType == type)
            {
                build.EnableFromBuilder = status;
            }
        }
    }
    
    public Build GetBuildByType(BuildingLabelTypes building)
    {        
        if (AvailableBuilds.Count > 0)
        {
            foreach (var build in AvailableBuilds)
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
        else
        {
            Global.Message($"Could not place building: {building.ToString()}");
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

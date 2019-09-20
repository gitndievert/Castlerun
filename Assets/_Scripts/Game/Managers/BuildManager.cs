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
    public List<Build> Builds;    

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
    
    private bool _buildPanelEnabled = false;
    private bool _troopPanelEnabled = false;

    /// <summary>
    /// Set the Build Panel On/Off
    /// </summary>
    public bool BuildPanelEnabled
    {
        get { return _buildPanelEnabled; }
        set
        {
            _buildPanelEnabled = value;
            BuildUI.BuildingsPanel.SetActive(_buildPanelEnabled);            
        }
    }

    /// <summary>
    /// Set the Troop Panel On/Off
    /// </summary>
    public bool TroopPanelEnabled
    {
        get { return _troopPanelEnabled; }
        set
        {
            _troopPanelEnabled = value;
            BuildUI.TroopsPanel.SetActive(_troopPanelEnabled);
        }
    }

    public void ShowTroopPanel()
    {
        BuildPanelEnabled = false;
        TroopPanelEnabled = true;
    }

    public void ShowBuildPanel()
    {
        BuildPanelEnabled = true;
        TroopPanelEnabled = false;
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
        BuildPanelEnabled = true;
        TroopPanelEnabled = false;
        RefreshBuilds();               
    }

    private void Update()
    {
        
        
    }

    public void RefreshBuilds()
    {
        int i = 0;
        int buildcount = Builds.Count;

        foreach (Transform trans in BuildUI.BuildingsPanel.transform)
        {
            if (i >= buildcount) break;
            var build = Builds[i];

            var image = trans.GetComponent<Image>();
            image.sprite = build.GetIcon();

            if (!build.EnableFromBuilder)
            {
                var color = image.color;
                color.r = 61f;
                color.g = 61f;
                color.b = 61f;
                color.a = 0.5f;
                image.color = color;
            }
            else
            {
                trans.GetComponent<Button>().onClick.AddListener(() => LoadByType(build.BuildingLabelType));
            }

            i++;
        }        
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

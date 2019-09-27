﻿// ********************************************************************
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
using SBK.Unity;
using TMPro;

public partial class UIManager : PSingle<UIManager>
{


    public Sprite DefaultIcon;

    public TextMeshProUGUI Messages;

    /// <summary>
    /// Inventory Panel
    /// </summary>
    [Header("Inventory")]
    public InventoryUI InventoryUIPanel;

    [Header("Player")]
    public ProgressBar HealthBar;

    public ProgressBar StaminaBar;
    /// <summary>
    /// Player Panel
    /// </summary>
    public PlayerUI PlayerUIPanel;

    /// <summary>
    /// Panel for selecting Buildings and Troops
    /// </summary>
    [Header("Building")]
    public BuildUI BuildingUIPanel;

 
    [Header("Selection System")]

    /// <summary>
    /// Target Panel
    /// </summary>
    public Transform TargetPanel;

    /// <summary>
    /// Selection Controller for all In Game Mouse Selections
    /// </summary>
    public Selection SelectableComponent;

    /// <summary>
    /// Target Box (Single Selection)
    /// </summary>    
    public SingleTargetBox SingleTargetBox;

    /// <summary>
    /// Multitarget Box Manager (All Selected Targets)
    /// </summary>    
    public MultiTargetBox MultiTargetBox;

    /// <summary>
    /// Enemy Target Box (Single Selection)
    /// </summary>    
    public SingleTargetBox EnemyTargetBox;



    protected override void PAwake()
    {
        if (SelectableComponent == null)
            SelectableComponent = GetComponent<Selection>();       
    }
    
    protected override void PDestroy()
    {
        
    }   
    
}

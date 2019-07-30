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
using SBK.Unity;
using TMPro;

public partial class UIManager : PSingle<UIManager>
{
    public ProgressBar HealthBar;
    public ProgressBar StaminaBar;

    /// <summary>
    /// Inventory Panel
    /// </summary>
    public InventoryUI InventoryUIPanel;

    /// <summary>
    /// Player Panel
    /// </summary>
    public PlayerUI PlayerUIPanel;

    /// <summary>
    /// Target Panel UI Text
    /// </summary>    
    public TargetUI TargetUI;
    
    /// <summary>
    /// Target Panel
    /// </summary>
    public Transform TargetPanel;

    public TextMeshProUGUI Messages;

    public Selectable SelectableComponent;

    protected override void PAwake()
    {
        if (SelectableComponent == null)
            SelectableComponent = GetComponent<Selectable>();
    }

    protected override void PDestroy()
    {
        
    }
    
}

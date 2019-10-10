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
using System.Collections.Generic;

public interface ISelectable : IBase
{
    bool IsSelected { get; set; }    
    void UnSelect();
    void Select();
    /// <summary>
    /// Gets all Properties on this Selectable Object
    /// </summary>
    GameObject GameObject { get; }
    void OnMouseDown();    
    string DisplayName { get; }    
    int GetCurrentHealth();
    int GetMaxHealth();
    HashSet<Troop> TargetingMe { get; set; }
    Player TargetByPlayer { get; set; }
    void SetTargetedByPlayer(Player player);
    bool IsDying { get; set; }
}
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

public class KeyBindings
{
    public const int LEFT_MOUSE_BUTTON = 0;
    public const int RIGHT_MOUSE_BUTTON = 1;
    public const int MIDDLE_MOUSE_BUTTON = 2;

    #region Movement
    public static KeyCode BuildKey1 = KeyCode.Alpha1;
    public static KeyCode BuildKey2 = KeyCode.Alpha2;
    public static KeyCode BuildKey3 = KeyCode.Alpha3;
    public static KeyCode BuildKey4 = KeyCode.Alpha4;
    public static KeyCode BuildKey5 = KeyCode.Alpha5;
    public static KeyCode Jump = KeyCode.Space;
    #endregion

    #region Dance Modes
    public static KeyCode Dance1 = KeyCode.G;
    #endregion

    public static KeyCode BattleToggle = KeyCode.Tab;
}

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

public static class Global
{
    public const bool DEVELOPER_MODE = false;

    #region Game Objects
    /// <summary>
    /// Global Tag for MultiCamRig
    /// </summary>
    public const string CAM_RIG_TAG = "CamRig";
    /// <summary>
    /// Global Tag for Basic Build Snappoints
    /// </summary>
    public const string SNAP_POINT_TAG = "SnapPoint";
    /// <summary>
    /// Global Tag for Resources
    /// </summary>
    public const string RESOURCE_TAG = "Resource";
    /// <summary>
    /// Build Area tag for specific BUILD AREA TRANSFORMS
    /// </summary>
    public const string BUILDAREA_TAG = "BuildArea";
    #endregion

    #region Selectable Objects
    /// <summary>
    /// Tag for Selectable Buildings
    /// </summary>
    public const string BUILD_TAG = "Build";

    /// <summary>
    /// Tag for selectable troops that are combat active
    /// </summary>
    public const string ARMY_TAG = "Army";

    /// <summary>
    /// Tag for Opponents Troops that are combat active
    /// </summary>
    public const string ENEMY_TAG = "Enemy";

    /// <summary>
    /// Tag for generic selectable troops that are NPC's (NON COMBAT)
    /// </summary>
    public const string NPC_TAG = "Npc";
    #endregion

    #region Game Constants

    /// <summary>
    /// Default layer for all players
    /// </summary>
    public const int PLAYER_LAYER = 14;

    public const int DEFAULT_LAYER = 0;
    public const int IGNORE_LAYER = 2;
    public const int GROUND_LAYER = 8;
    public const int ARMY_LAYER = 12;
    public const int PROJECTILE_LAYER = 12;
    public const int UI_LAYER = 5;    
    public const int BUILD_GRID_LAYER = 9;

    /// <summary>
    /// Max Number of Player Slots per game
    /// </summary>
    public const int PLAYER_MAX_SLOTS = 4;
    #endregion

    #region Player Flags
    public static bool BuildMode = false;
    public static bool MouseLook = false;
    //public static bool BattleMode = false;
    public static bool InsideCastle = false;
    #endregion

    #region Gameplay Stats
    //Player
    public static int PlayerMoveSpeedBonus = 0;
    public static int PlayerHealthBonus = 0;

    //Troop
    public static float ConstructionSpeedBonus = 1f;
    public static int TroopHealthBonus = 0;
    public static int TroopAttackBonus = 0;    
    #endregion


    public static void Message(string message)
    {
        UIManager.Instance.Messages.color = Color.white;
        UIManager.Instance.Messages.text = message;        
    }    

    public static void Message(string message, Color color)
    {
        UIManager.Instance.Messages.color = color;
        UIManager.Instance.Messages.text = message;
    }
    
}



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

public static class Global
{
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

    #region Game Constants
    public const int DEFAULT_LAYER = 0;
    public const int IGNORE_LAYER = 2;
    public const int GROUND_LAYER = 8;
    public const float STRIKE_DIST = 10f;
    public const int BUILD_GRID_LAYER = 9;

    public const int PLAYER_MAX_SLOTS = 4;
    #endregion

    #region Player Flags
    public static bool BuildMode = false;
    public static bool MouseLook = false;
    public static bool BattleMode = false;
    #endregion
}

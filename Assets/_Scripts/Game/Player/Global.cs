using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion
    
    #region Game Constants
    public const int DEFAULT_LAYER = 0;
    public const int IGNORE_LAYER = 2;
    public const int GROUND_LAYER = 8;
    public const float STRIKE_DIST = 10f;
    public const int BUILD_GRID_LAYER = 9;
    #endregion

    #region Player Flags
    public static bool BuildMode = false;
    public static bool MouseLook = false;
    public static bool BattleMode = false;
    #endregion
}

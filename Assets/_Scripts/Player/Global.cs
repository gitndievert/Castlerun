using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{

    #region Game Objects
    public const string CAM_RIG_TAG = "CamRig";
    public const string SNAP_POINT_TAG = "SnapPoint";

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
    #endregion
}

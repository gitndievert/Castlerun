using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleProp : BaseResource
{
    public string Name;

    protected override void Awake()
    {
        
    }

    private void Update()
    {
        

    }

    /// <summary>
    /// Level of the Castle (levels 1 to 3)
    /// </summary>
    [Range(1,3)]   
    public int CastleLevel;
    
    #region BonusMoveSpeed
    public bool HasBonusMoveSpeed = false;
    public float BonusMoveSpeed = 1f;
    #endregion

    #region BonusBuildSpeed
    public bool HasBonusBuildSpeed = false;
    public float BonusBuildSpeed = 1f;
    #endregion

    #region BonusLowerCost
    public bool HasBonusLowerCost = false;
    public float BonusLowerCost = .5f;
    #endregion

    public bool HasArchers = false;
    public bool HasGuards = false;

    public Color CastleColorHue { get; private set; }

    /// <summary>
    /// !!!TODO!!! - For Jessia
    /// Come back later, This will change the color on the rend for the castles
    /// </summary>
    /// <param name="color"></param>
    private void SetColor(Color color)
    {
        
    }
}




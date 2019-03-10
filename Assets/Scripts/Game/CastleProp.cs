using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleProp : BaseResource
{
    public string Name;
    public StatModifier StatModifier;

    protected override void Awake()
    {
        
    }

    protected override void Start()
    {
        //StatModifier = GetPlayer(1).StatsModifier;
        //When castle is create we need to popoulate the stats on its player
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
    protected float _BonusMoveSpeed = 1f;
    #endregion

    #region BonusBuildSpeed
    public bool HasBonusBuildSpeed = false;
    protected float _BonusBuildSpeed = 1f;
    #endregion

    #region BonusLowerCost
    public bool HasBonusLowerCost = false;
    protected float BonusLowerCost = .5f;
    #endregion

    protected bool HasArchers = false;
    protected bool HasGuards = false;

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




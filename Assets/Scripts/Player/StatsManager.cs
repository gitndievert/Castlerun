using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{    
    public static readonly PlayerStats Player = new PlayerStats();        
        
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {        
        Player.Health = 100;
        Player.MoveSpeed = 1f;
        Player.BuildSpeed = 1f;
    }

    public PlayerStats UpdateBonus(PlayerStats stats)
    {
        Player.Health += stats.Health;        
        Player.MoveSpeed += stats.MoveSpeed;        
        Player.BuildSpeed += stats.BuildSpeed;
        
        return Player;
    }

    public PlayerStats UpdateBonus(CastleStats stats)
    {
        Player.CastleStats = stats;
        Player.Health += stats.BonusHealth;
        Player.MoveSpeed += stats.BonusMoveSpeed;
        return Player;
    }
             
}


[Serializable]
public class PlayerStats : IStat
{
    public float Health;
    public float MoveSpeed;
    public float BuildSpeed;
    public CastleStats CastleStats;
}

[Serializable]
public class CastleStats : IStat
{
    public bool HasArchers = false;
    public bool HasGuards = false;
    public float BonusHealth;
    public float BonusMoveSpeed;
    public float BonusLowerCost;
}
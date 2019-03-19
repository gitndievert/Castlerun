﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{    
    public static readonly PlayerStats Player = new PlayerStats();    
    public static Stack<string> BonusList = new Stack<string>();
        
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
        if (stats.Health != 0)
        {
            Player.Health += stats.Health;
            BonusList.Push($"+{stats.Health} to player health");
        }
        if (stats.MoveSpeed != 0)
        {
            Player.MoveSpeed *= stats.MoveSpeed;
            BonusList.Push($"Player speed x {stats.MoveSpeed}");
        }
        if (stats.BuildSpeed != 0)
        {
            Player.BuildSpeed *= stats.BuildSpeed;
            BonusList.Push($"Build speed x {stats.BuildSpeed}");
        }
        return Player;
    }

    public PlayerStats UpdateBonus(CastleStats stats)
    {
        Player.CastleStats = stats;        
        if (stats.BonusHealth != 0)
        {
            Player.Health += stats.BonusHealth;
            BonusList.Push($"Castle Bonus +{stats.BonusHealth} to player health");
        }
        if (stats.BonusMoveSpeed != 0)
        {
            Player.MoveSpeed *= stats.BonusMoveSpeed;
            BonusList.Push($"Castle Bonus player speed x {stats.BonusMoveSpeed}");
        }
        if(stats.BonusLowerCost != 0)        
            BonusList.Push($"Castle Bonus build costs reduced by {stats.BonusLowerCost}%");        
        if(stats.HasArchers)
            BonusList.Push($"Castle Bonus - Archers");
        if(stats.HasGuards)
            BonusList.Push($"Castle Bonus - Guards");

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
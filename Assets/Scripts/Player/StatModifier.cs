using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier : MonoBehaviour
{
    public static Stats CurrentStats { get; private set; }

    private void Awake()
    {
        CurrentStats = new Stats()
        {
            Health = 100,
            MoveSpeed = 1f,
            BuildSpeed = 1f,
            BonusMoveSpeed = 1f,
            BonusLowerCost = 0.5f,
            HasArchers = false,
            HasGuards = false
        };

    }

    public Stats AddModifiers(Stats stats)
    {
        CurrentStats = stats;
        return CurrentStats;
    }

    public Stats RemoveModifiers(Stats stats)
    {
        CurrentStats = stats;
        return CurrentStats;
    }
}

public class Stats
{
    public float Health { get; set; }
    public float MoveSpeed { get; set; }
    public float BuildSpeed { get; set; }
    public float BonusMoveSpeed { get; set; }
    public float BonusLowerCost { get; set; }
    public bool HasArchers { get; set; }
    public bool HasGuards { get; set; }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatManager : MonoBehaviour
{

}
    

[Serializable]
public class PlayerStats : IStat
{
    public float Health;
    public float MoveSpeed;
    public float BuildSpeed;
    public int HitAmount;
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
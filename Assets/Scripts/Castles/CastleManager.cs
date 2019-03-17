using SBK.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CastleManager : PSingle<CastleManager>
{
    /// <summary>
    /// All Castles in Game
    /// </summary>
    [Tooltip("All the castle prefabs in the game")]
    public List<Castle> CastleList;
    public Transform[] SpawnPads;

    protected override void PAwake()
    {
        if (CastleList != null && CastleList.Count == 0)
            CastleList.AddRange(transform.GetComponentsInChildren<Castle>());
    }

    protected override void PDestroy()
    {
        
    }

    private void Start()
    {
        
    }

    public Transform GetSpawnPad(int playerNumber)
    {
        if (SpawnPads.Length > 0)
        {
            foreach (var pad in SpawnPads)
            {
                var id = pad.name.Last();
                int t_num = int.Parse(id.ToString());
                if (t_num == playerNumber)
                    return pad;
                else
                    throw new System.Exception("Map does not have pads mapped for all players");
            }
        }

        return null;
    }

    public Castle GetCastlebyName(string name, int level)
    {
        foreach (var castle in CastleList)
        {
            if (castle.name == name && castle.Level == level)
                return castle;
        }

        return null;
    }

    public Castle GetCastleByType(CastleType type, int level)
    {
        foreach(var castle in CastleList)
        {
            if (castle.CastleType == type && castle.Level == level)
                return castle;
        }

        return null;
    }

    public void SpawnCastle(Player player, int castleLevel)
    {
        var stats = player.StatsModifier;
        var spawnpad = GetSpawnPad(player.PlayerNumber);



    }
}

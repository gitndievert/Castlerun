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
    public PlayerPad[] SpawnPads;    

    public Castle Player1Castle = null;
    public Castle Player2Castle = null;
    public Castle Player3Castle = null;
    public Castle Player4Castle = null;

    protected override void PAwake()
    {
        if (CastleList != null && CastleList.Count == 0)
            CastleList.AddRange(transform.GetComponentsInChildren<Castle>());
    }

    protected override void PDestroy()
    {
        
    }
  
    public PlayerPad GetSpawnPad(int playerNumber)
    {
        if (SpawnPads.Length > 0)
        {
            foreach (var pad in SpawnPads)
            {                
                if (!pad.gameObject.activeSelf) continue;
                var id = pad.name.Last();
                int t_num = int.Parse(id.ToString());
                if (t_num == playerNumber) return pad;                
            }
        }

        return null;
    }

    public Castle GetCastlebyName(string name)
    {
        foreach (var castle in CastleList)
        {
            if (castle.name == name)
                return castle;
        }

        return null;
    }

    public Castle GetCastleByType(CastleType type)
    {
        foreach(var castle in CastleList)
        {
            if (castle.CastleType == type)
                return castle;
        }

        return null;
    }

    public void SpawnCastle(CastleType type, Player player)
    {
        SpawnCastle(GetCastleByType(type), player);
    }
    
    public void SpawnCastle(Castle castle, Player player)
    {        
        var playerPad = GetSpawnPad(player.PlayerNumber);
        if (playerPad)
        {
            var castleObj = Instantiate(castle.gameObject);
            
            if (player.PlayerNumber == 1)
                Player1Castle = castle;
            if (player.PlayerNumber == 2)
                Player2Castle = castle;
            if (player.PlayerNumber == 3)
                Player3Castle = castle;
            if (player.PlayerNumber == 4)
                Player4Castle = castle;
            
            castleObj.transform.position = playerPad.CastleSpawnPosition;
            castleObj.transform.rotation = playerPad.CastleRotation;
        }

    }
}

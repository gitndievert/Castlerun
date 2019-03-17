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

    public Castle GetCastleByType(CastleType type)
    {
        foreach (var castle in CastleList)
        {
            if (castle.CastleType == type)
                return castle;
        }

        return null;
    }

    public void SpawnCastle(Castle castle, Player player)
    {
        var stats = player.StatsModifier;
        var spawnPad = GetSpawnPad(player.PlayerNumber);
        Bounds padBounds = spawnPad.GetComponent<MeshFilter>().mesh.bounds;

        var castleObj = Instantiate(castle.gameObject, spawnPad, false);

        if (player.PlayerNumber == 1)
            Player1Castle = castle;
        if (player.PlayerNumber == 2)
            Player2Castle = castle;
        if (player.PlayerNumber == 3)
            Player3Castle = castle;
        if (player.PlayerNumber == 4)
            Player4Castle = castle;

        //Align castle to ground
        //(transform.gameObject.transform.localScale.y/2)
        castleObj.transform.position = new Vector3(castleObj.transform.position.x,
            spawnPad.position.y + 3f, castleObj.transform.position.z);

        /*
         * Transform car;
            Bounds carBounds = car.GetComponent<MeshFilter>().mesh.bounds;
            Vector3 whereYouWantMe;
            Vector3 offset = car.transform.position - car.transform.TransformPoint(carBounds.center);
            car.transform.position = whereYouWantMe + offset;*/


    }
}

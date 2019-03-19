using SBK.Unity;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PSingle<GameManager>
{
    public List<Player> Players;
    
    public Player GetPlayer(int position)
    {
        return Players.ElementAt(position);
    }

    public Player GetPlayer(string name)
    {
        return Players.Where(p => p.PlayerName == name).First();
    }

    public Player MatchPlayer(Player player)
    {
        return Players.Where(p => p.Equals(player)).First();
    }

    protected override void PAwake()
    {
        foreach(var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(player.activeInHierarchy)
            {
                Players.Add(player.GetComponent<Player>());
            }
        }
        
    }

    protected override void PDestroy()
    {
        
    }
}

using SBK.Unity;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class GameManager : PSingle<GameManager>
{
    public GameObject Camera;
    public GameObject PlayerInstance;
    public PlayerPad[] PlayerPads;
    public List<Player> Players;

    private int _numOfPlayer;
    
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

    public void SetNumberOfPlayers(int num)
    {
        _numOfPlayer = num;
    }

    protected override void PAwake()
    {
        //FOR TESTING        
        PlayerPads = gameObject.GetComponentsInChildren<PlayerPad>();        
        SetNumberOfPlayers(PlayerPads.Length);
        
    }

    private void Start()
    {
        if (Camera == null)
            throw new System.Exception("Must have a Camera Rig set in the Game Manager scripts");
        for (int i = 1; i <= _numOfPlayer; i++)
        {
            var character = Instantiate(PlayerInstance, PlayerPads[i - 1].PlayerSpawnPosition, Quaternion.identity);
            var player = character.GetComponent<Player>();
            player.PlayerNumber = i;
            Players.Add(player);

            //Will need to be totally redone with the multiplayer code
            if (player.PlayerNumber == 1)
            {
                Transform pt = player.transform;                
                //Having issues with autocam, so removing it
                //CameraRig.GetComponent<AutoCam>().SetTarget(pt);
                Camera.GetComponent<CameraRotate>().target = pt;
            }
        }                
    }

    protected override void PDestroy()
    {
        
    }


}

using SBK.Unity;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;
using UnityEngine.SceneManagement;

public class GameManager : PSingle<GameManager>
{
    public GameObject CameraRig = null;

    public GameObject PlayerInstance;
    public PlayerPad[] PlayerPads;
    public List<Player> Players;

    private int _numOfPlayer;
       
    public Player GetMyPlayer()
    {
        return GetPlayer(0);
    }

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
        if (CameraRig == null)
            throw new System.Exception("Must have a Camera Rig Hooked Up on the Game Manager");
        StartPlayersTest();
        Music.Instance.PlayMusicTrack(1);
    }

    protected override void PDestroy()
    {
        
    }

    private void StartPlayersTest()
    {
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
                CameraRig.GetComponent<CameraRotate>().target = pt;
                //CameraRig.GetComponent<BasicCameraBehaviour>().SetPlayerTarget = pt;
            }
        }
    }




}
